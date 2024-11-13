using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderButtonsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] objectsList;
    GameObject objectSelected;
    GameObject objectCreated;
    string selectedItem;
    //Pop effect
    [SerializeField] float popAnimDuration;
    public void ShelfBags()
    {
        selectedItem = "estanter�a";
        SelectItem();
    }
    public void ShelfBoxes()
    {
        selectedItem = "estanter�a2";
        SelectItem();
    }

    //Seleccionar Item y Crearlo
    void SelectItem()
    {
        Destroy(objectCreated); //DEBUG USE ONLY
        if (selectedItem == "estanter�a")
        {
            objectSelected = objectsList[0];
            objectSelected.transform.LeanScale(Vector3.zero, 0);
        }
        if (selectedItem == "estanter�a2")
        {
            objectSelected = objectsList[1];
            objectSelected.transform.LeanScale(Vector3.zero, 0);
        }
        PlaceItem();
    }

    void PlaceItem()
    {
        objectCreated = Instantiate(objectSelected, Vector3.zero, Quaternion.identity);
        objectCreated.transform.LeanScale(new Vector3(1, 1, 1), popAnimDuration).setEase(LeanTweenType.easeOutBack);
    }
}
