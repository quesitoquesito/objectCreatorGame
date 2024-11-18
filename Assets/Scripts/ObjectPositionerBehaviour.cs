using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectPositionerBehaviour : MonoBehaviour
{
    [SerializeField] AnimationsBehaviour animationsBehaviour;
    [SerializeField] UIBehaviour uiBehaviour;
    public SliderButtonsBehaviour sliderBehaviour;
    bool selectingObject;
    public bool isObjectMoving;
    public GameObject movingObject;
    [SerializeField] public Transform selectedToMoveParent;
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
            }
        }
    }
    public void MoveObject()
    {
        selectingObject = true;
        uiBehaviour.showButtonsArea.interactable = false;
        uiBehaviour.ShowOptionsMenu();
    }
}
