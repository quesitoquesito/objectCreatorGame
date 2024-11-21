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
    [SerializeField] GameObject[] shelfBagsObjects;
    [SerializeField] GameObject[] shelfEndObjects;
    [SerializeField] GameObject[] cauldronObjects;

    GameObject objectSelected;
    [HideInInspector] public GameObject objectCreated;
    int selectedItem;

    [SerializeField] ObjectPositionerBehaviour positionerBehaviour;
    [SerializeField] ObjectRotatorBehaviour rotatorBehaviour;
    [SerializeField] GameObject closeSliderObj;

    //Seleccionar Item y Crearlo
    public void PlaceItem(int ButtonID)
    {
        for (int i = 0; i < sideBarBehaviour.sliderButtons.Length; i++)
        {
            sideBarBehaviour.sliderButtons[i].interactable = false;
        }
        sideBarBehaviour.ShowSlider();
        if (ButtonID == 1) { objectSelected = shelfBagsObjects[Random.Range(0, shelfBagsObjects.Length)]; }
        else if (ButtonID == 2) { objectSelected = shelfEndObjects[Random.Range(0, shelfEndObjects.Length)]; }
        else if (ButtonID == 6) { objectSelected = cauldronObjects[Random.Range(0, cauldronObjects.Length)]; }
        else objectSelected = objectsList[ButtonID];
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
