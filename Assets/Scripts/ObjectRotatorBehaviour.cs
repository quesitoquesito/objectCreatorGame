using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectRotatorBehaviour : MonoBehaviour
{
    [SerializeField] UIBehaviour uiBehaviour;
    [SerializeField] SliderButtonsBehaviour sliderBehaviour;
    public GameObject objectRotating;
    public bool isObjectRotating;
    bool selectingObject;
    float rotation;
    [SerializeField] float rotationAmount;
    Transform selectedToRotateParent;
    private void Start()
    {
        isObjectRotating = false;
    }
    void Update()
    {
        if (isObjectRotating)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && !objectRotating.gameObject.CompareTag("CannotModify"))
            {
                rotation = -Input.mouseScrollDelta.y * rotationAmount;
                if (selectedToRotateParent != null)
                {
                    selectedToRotateParent.transform.Rotate(0, rotation, 0);
                }
                else objectRotating.gameObject.transform.Rotate(0, rotation, 0);
            }
            if (Input.GetMouseButtonDown(0))
            {
                isObjectRotating = false;
                selectingObject = false;
                uiBehaviour.showButtonsArea.interactable = true;
            }
        }
        if (selectingObject)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                selectedToRotateParent = hit.collider.gameObject.transform.parent;
                objectRotating = hit.collider.gameObject;
                isObjectRotating = true;
            }
        }
    }
    public void RotateObject()
    {
        selectingObject = true;
        uiBehaviour.showButtonsArea.interactable = false;
        uiBehaviour.ShowOptionsMenu();
    }
}
