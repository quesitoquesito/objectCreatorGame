using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ObjectPositionerBehaviour : MonoBehaviour
{
    [SerializeField] PopUpsBehaviour popUpsBehaviour;
    [SerializeField] AnimationsBehaviour animationsBehaviour;
    [SerializeField] UIBehaviour uiBehaviour;
    public SliderButtonsBehaviour sliderBehaviour;
    bool selectingObject;
    public bool isObjectMoving;
    //Objeto seleccionado para mover
    [HideInInspector] public GameObject movingObject;
    //Si se detecta que un objeto tiene un pariente, se hacen los cambios en ese pariente
    [HideInInspector] public Transform selectedToMoveParent;
    private void Start()
    {
        isObjectMoving = false;
        selectingObject = false;
    }
    void Update()
    {
        if (isObjectMoving)
        {
            movingObject.SetActive(false);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (selectedToMoveParent != null)
                {
                    selectedToMoveParent.transform.position = hit.point;
                }
                else
                {
                    movingObject.transform.position = hit.point;
                }
                animationsBehaviour.areaShadow.transform.position = hit.point;
            }
            movingObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedToMoveParent != null)
                {
                    animationsBehaviour.isParent = true;
                }
                else
                {
                    animationsBehaviour.isParent = false;
                }
                isObjectMoving = false;
                animationsBehaviour.showShadowArea = false;
                animationsBehaviour.AreaShadowAnimation();
                animationsBehaviour.SetObjectAnimation();
            }
        }
        if (selectingObject)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && Input.GetMouseButtonDown(0) && !hit.collider.gameObject.CompareTag("CannotModify"))
            {
                selectedToMoveParent = hit.collider.gameObject.transform.parent;
                movingObject = hit.collider.gameObject;
                selectingObject = false;
                isObjectMoving = true;
                animationsBehaviour.AreaShadowAnimation();
            }
        }
    }
    public void MoveObject()
    {
        popUpsBehaviour.activateMovingPopUp = true;
        popUpsBehaviour.MovingPopUp();
        selectingObject = true;
        uiBehaviour.showButtonsArea.interactable = false;
        uiBehaviour.ShowOptionsMenu();
        animationsBehaviour.showShadowArea = true;
    }
}
