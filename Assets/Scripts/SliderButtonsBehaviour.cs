using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;

public class SliderButtonsBehaviour : MonoBehaviour
{
    [SerializeField] AnimationsBehaviour animationsBehaviour;
    [SerializeField] UISideBarBehaviour sideBarBehaviour;
    [SerializeField] GameObject[] objectsList;
    GameObject objectSelected;
    public GameObject objectCreated;
    int selectedItem;

    [SerializeField] ObjectPositionerBehaviour positionerBehaviour;
    [SerializeField] ObjectRotatorBehaviour rotatorBehaviour;
    [SerializeField] GameObject closeSliderObj;
    public void ShelfBags()
    {
        selectedItem = 0;
        PlaceItem();
    }
    public void ShelfBoxes()
    {
        selectedItem = 1;
        PlaceItem();
    }

    //Seleccionar Item y Crearlo
    void PlaceItem()
    {
        for (int i = 0; i < sideBarBehaviour.sliderButtons.Length; i++)
        {
            sideBarBehaviour.sliderButtons[i].interactable = false;
        }
        sideBarBehaviour.ShowSlider();
        objectSelected = objectsList[selectedItem];
        objectSelected.transform.LeanScale(Vector3.zero, 0);
        objectCreated = Instantiate(objectSelected, new Vector3 (3, 2, 2), Quaternion.identity);
        objectCreated.transform.LeanScale(Vector3.one, animationsBehaviour.popAnimDuration).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            LeanTween.moveX(objectCreated, objectCreated.transform.position.x, 0.5f).setOnComplete(() =>
            {
                positionerBehaviour.movingObject = objectCreated;
                rotatorBehaviour.objectRotating = objectCreated;
                rotatorBehaviour.isObjectRotating = true;
                animationsBehaviour.PopUpAnimation();
            });
        });
    }
}
