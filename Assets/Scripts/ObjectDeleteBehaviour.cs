using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleteBehaviour : MonoBehaviour
{
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
                    deleting = false;
                    animationsBehaviour.DeleteAnimation();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    deleting = false;
                    uiBehaviour.showButtonsArea.interactable = true;
                }
            }
        }
    }
    public void Deleting()
    {
        deleting = true;
        uiBehaviour.showButtonsArea.interactable = false;
        uiBehaviour.ShowOptionsMenu();
    }
}
