using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderButtonsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] objectsList;
    GameObject objectSelected;
    GameObject objectCreated;
    int selectedItem;
    //Pop effect
    [SerializeField] float popAnimDuration;
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
        Destroy(objectCreated); //DEBUG USE ONLY
        objectSelected = objectsList[selectedItem];
        objectSelected.transform.LeanScale(Vector3.zero, 0);
        objectCreated = Instantiate(objectSelected, Vector3.zero, Quaternion.identity);
        objectCreated.transform.LeanScale(new Vector3(1, 1, 1), popAnimDuration).setEase(LeanTweenType.easeOutBack);
    }
}
