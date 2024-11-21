using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleBehaviour : MonoBehaviour
{
    [SerializeField] PopUpsBehaviour popUpsBehaviour;
    [SerializeField] UIBehaviour uiBehaviour;
    [SerializeField] float scaleAmount;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    [SerializeField] float valueDivide;
    GameObject objectScaling;
    Transform selectedToScaleParent;
    bool isScaling;
    bool selectingObject;
    float scale;
    float initialMousePosY;
    
    void Update()
    {
        if (isScaling)
        {
            scale = ((Input.mousePosition.y - initialMousePosY) + scaleAmount) / valueDivide;
            if (selectedToScaleParent != null)
            {
                if (scale >= minScale && scale <= maxScale)
                {
                    selectedToScaleParent.transform.localScale = new Vector3(scale, scale, scale);
                }
            }
            else
            {
                if (scale >= minScale && scale <= maxScale)
                {
                    objectScaling.gameObject.transform.localScale = new Vector3(scale, scale, scale);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                popUpsBehaviour.activateScalingPopUp = false;
                popUpsBehaviour.ScalingPopUp();
                isScaling = false;
                selectingObject = false;
            }
        }
        if (selectingObject)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && Input.GetMouseButtonDown(0) && !hit.collider.gameObject.CompareTag("CannotModify"))
            {
                selectedToScaleParent = hit.collider.gameObject.transform.parent;
                objectScaling = hit.collider.gameObject;
                isScaling = true;
                initialMousePosY = Input.mousePosition.y;
                selectingObject = false;
            }
        }
    }
    public void ScaleObject()
    {
        popUpsBehaviour.activateScalingPopUp = true;
        popUpsBehaviour.ScalingPopUp();
        selectingObject = true;
        uiBehaviour.showButtonsArea.interactable = false;
        uiBehaviour.ShowOptionsMenu();
    }
}
