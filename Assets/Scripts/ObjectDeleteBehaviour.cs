using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleteBehaviour : MonoBehaviour
{
    [SerializeField] PopUpsBehaviour popUpsBehaviour;
    [SerializeField] AnimationsBehaviour animationsBehaviour;
    [SerializeField] UIBehaviour uiBehaviour;
    public bool deleting;
    void Start()
    {
        deleting = false;
    }

    void Update()
    {
        if (deleting)
        {
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (Input.GetMouseButtonDown(0) && !hit.collider.gameObject.CompareTag("CannotModify"))
                {
                    animationsBehaviour.selectedToDeleteTemp = hit.collider.gameObject;
                    Transform selectedToDeleteParent = hit.collider.gameObject.transform.parent;
                    if (selectedToDeleteParent != null && !selectedToDeleteParent.CompareTag("CannotModify"))
                    {
                        animationsBehaviour.isParent = true;
                    }
                    else
                    {
                        animationsBehaviour.isParent = false;
                    }
                    deleting = false;
                    animationsBehaviour.DeleteAnimation();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    popUpsBehaviour.activateDeletingPopUp = false;
                    popUpsBehaviour.DeletingPopUp();
                    deleting = false;
                }
            }
        }
    }
    public void Deleting()
    {
        popUpsBehaviour.activateDeletingPopUp = true;
        popUpsBehaviour.DeletingPopUp();
        deleting = true;
        uiBehaviour.showButtonsArea.interactable = false;
        uiBehaviour.ShowOptionsMenu();
    }
}
