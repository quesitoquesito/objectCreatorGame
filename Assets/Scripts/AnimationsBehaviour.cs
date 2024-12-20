using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationsBehaviour : MonoBehaviour
{
    //PopUps
    [SerializeField] PopUpsBehaviour popUpsBehaviour;
    //UIBehaviour
    [SerializeField] UIBehaviour uiBehaviour;
    public LeanTweenType animUp;
    public LeanTweenType animDown;
    public LeanTweenType buttonBackgroundAnimUp;
    public LeanTweenType buttonBackgroundAnimDown;
    public LeanTweenType buttonScaleAnimAppear;
    public LeanTweenType buttonScaleAnimDisappear;
    [HideInInspector] public LeanTweenType buttonScaleAnim;
    [HideInInspector] public LeanTweenType animVertical;
    public float timeBetweenAnim;
    public float animSpeed;
    [SerializeField] float scaleButtonAnimSpeed;

    //UISideBarBehaviour
    [SerializeField] UISideBarBehaviour uiSideBarBehaviour;
    public float sliderAnimDuration;
    [HideInInspector] public float sliderAnimDurationActual;
    public LeanTweenType sliderAnimTypeUp;
    public LeanTweenType sliderAnimTypeDown;
    [HideInInspector] public LeanTweenType sliderAnim;
    [SerializeField] float appearDelay;

    //ObjectPositionerBehaviour
    [SerializeField] ObjectPositionerBehaviour objectPositionerBehaviour;
    public float popAnimDuration;
    [SerializeField] float setObjectMaxSize;
    GameObject selectedToMove;
    Vector3 selectedToMoveOGScale;

    //ObjectDeleteBehaviour
    [SerializeField] ObjectDeleteBehaviour objectDeleteBehaviour;
    [HideInInspector] public GameObject selectedToDeleteTemp;
    [SerializeField] float deleteObjectMaxSize;
    [HideInInspector] public bool isParent;
    Transform selectedToDeleteParent;

    //ShadowArea
    public GameObject areaShadow;
    [HideInInspector] public bool showShadowArea;
    [SerializeField] float shadowAnimSpeed;
    [SerializeField] float shadowScale;
    [SerializeField] LeanTweenType shadowAnimAppear;
    [SerializeField] LeanTweenType shadowAnimDisappear;
    LeanTweenType shadowNeededAnim;
    Vector3 shadowNeededScale;



    private void Start()
    {
        showShadowArea = false;
        areaShadow.transform.localScale = Vector3.zero;
        uiBehaviour.buttonScale.transform.localScale = Vector3.zero;
        areaShadow.SetActive(true);
    }

    //UIBehaviour
    public void ShowOptionsMenuAnimation()
    {
        LeanTween.moveX(uiBehaviour.buttonCreate, uiBehaviour.buttonCreate.transform.position.x, timeBetweenAnim).setOnComplete(() =>
        {
            LeanTween.moveLocalY(uiBehaviour.buttonCreate, uiBehaviour.yButtonPos, animSpeed).setEase(animVertical);
            LeanTween.moveX(uiBehaviour.buttonCreate, uiBehaviour.buttonCreate.transform.position.x, timeBetweenAnim).setOnComplete(() =>
            {
                LeanTween.moveLocalY(uiBehaviour.buttonMove, uiBehaviour.yButtonPos, animSpeed).setEase(animVertical);
            });
            LeanTween.moveX(uiBehaviour.buttonCreate, uiBehaviour.buttonCreate.transform.position.x, timeBetweenAnim * 2).setOnComplete(() =>
            {
                LeanTween.moveLocalY(uiBehaviour.buttonRotate, uiBehaviour.yButtonPos, animSpeed).setEase(animVertical);
            });
            LeanTween.moveX(uiBehaviour.buttonCreate, uiBehaviour.buttonCreate.transform.position.x, timeBetweenAnim * 3).setOnComplete(() =>
            {
                LeanTween.moveX(uiBehaviour.buttonCreate, uiBehaviour.buttonCreate.transform.position.x, timeBetweenAnim).setOnComplete(() =>
                {
                    LeanTween.moveLocalY(uiBehaviour.buttonsBackground, uiBehaviour.yButBackgroundPos, animSpeed).setEase(buttonBackgroundAnimDown); //Animaci�n del fondo de botones.
                });
                LeanTween.moveLocalY(uiBehaviour.buttonDelete, uiBehaviour.yButtonPos, animSpeed).setEase(animVertical).setOnComplete(() =>
                {
                    LeanTween.scale(uiBehaviour.buttonScale, new Vector3(uiBehaviour.buttonScaleScale, uiBehaviour.buttonScaleScale, uiBehaviour.buttonScaleScale), scaleButtonAnimSpeed).setEase(buttonScaleAnim);
                    for (int i = 0; i < uiBehaviour.menuButtons.Length; i++)
                    {
                        uiBehaviour.menuButtons[i].interactable = true;
                    }
                    if (uiBehaviour.create)
                    {
                        uiBehaviour.create = false;
                        uiBehaviour.showButtonsArea.interactable = false;
                        uiBehaviour.sideBarObj.GetComponent<UISideBarBehaviour>().ShowSlider();
                    }
                    else if (!uiBehaviour.create)
                    {
                        uiBehaviour.showButtonsArea.interactable = true;
                    }
                });
            });
        });
    }

    //UISideBarBehaviour
    public void ShowSliderAnimation()
    {
        LeanTween.moveY(uiSideBarBehaviour.gameObject, uiSideBarBehaviour.gameObject.transform.position.y, appearDelay).setOnComplete(() =>
        {
            LeanTween.moveLocalY(uiSideBarBehaviour.gameObject, uiSideBarBehaviour.heightNeeded, sliderAnimDurationActual).setEase(sliderAnim).setOnComplete(() =>
            {
                //Comprueba si el slider est� subiendo para activar el bot�n para los botones del men�.
                if (uiSideBarBehaviour.sliderActive)
                {
                    if (popUpsBehaviour.activateCreatingPopUp)
                    {
                        popUpsBehaviour.center = true;
                        popUpsBehaviour.CenterCreatePopUp();
                    }
                    uiSideBarBehaviour.areaButton.interactable = true;
                    uiSideBarBehaviour.sliderActive = false;
                    for (int i = 0; i < uiSideBarBehaviour.sliderButtons.Length; i++)
                    {
                        uiSideBarBehaviour.sliderButtons[i].interactable = false;
                    }
                }
                //Comprueba si el slider est� bajando y activa el bot�n (invisible) para cerrarlo tras terminar la animaci�n, tambi�n deja un delay para cerrar el panel.
                else if (!uiSideBarBehaviour.sliderActive)
                {
                    LeanTween.moveX(uiSideBarBehaviour.gameObject, uiSideBarBehaviour.gameObject.transform.position.x, appearDelay).setOnComplete(() =>
                    {
                        uiSideBarBehaviour.closeSliderObj.SetActive(true);
                        uiSideBarBehaviour.sliderActive = true;
                        for (int i = 0; i < uiSideBarBehaviour.sliderButtons.Length; i++)
                        {
                            uiSideBarBehaviour.sliderButtons[i].interactable = true;
                        }
                    });
                }
            });
        });
    }

    //ObjectPositionerBehaviour
    public void PopUpAnimation()
    {
        LeanTween.scale(objectPositionerBehaviour.movingObject, Vector3.zero, popAnimDuration).setEase(LeanTweenType.easeOutQuint).setOnComplete(() =>
        {
            LeanTween.moveX(objectPositionerBehaviour.movingObject, objectPositionerBehaviour.movingObject.transform.position.x, popAnimDuration).setOnComplete(() =>
            {
                objectPositionerBehaviour.movingObject.SetActive(false);
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    objectPositionerBehaviour.movingObject.transform.position = hit.point;
                }
                objectPositionerBehaviour.movingObject.SetActive(true);
                objectPositionerBehaviour.movingObject.transform.LeanScale(Vector3.one, popAnimDuration).setEase(LeanTweenType.easeOutBack);
                objectPositionerBehaviour.isObjectMoving = true;
                showShadowArea = true;
                AreaShadowAnimation();
            });
        });
    }
    public void SetObjectAnimation()
    {
        if (isParent)
        {
            selectedToMove = objectPositionerBehaviour.selectedToMoveParent.gameObject;
        }
        else selectedToMove = objectPositionerBehaviour.movingObject;
        selectedToMoveOGScale = selectedToMove.transform.localScale;
        LeanTween.scale(selectedToMove, new Vector3(selectedToMoveOGScale.x + setObjectMaxSize, selectedToMoveOGScale.y + setObjectMaxSize, selectedToMoveOGScale.z + setObjectMaxSize), 0.5f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.rotateAround(selectedToMove, Vector3.up, selectedToMove.transform.rotation.y - 360, 1f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.moveX(selectedToMove, selectedToMove.transform.position.x, 0.5f).setOnComplete(() =>
        {
            LeanTween.scale(selectedToMove, selectedToMoveOGScale, 0.5f).setEase(LeanTweenType.easeOutBack);
            selectedToMove.transform.Rotate(Vector3.zero);
            objectPositionerBehaviour.selectedToMoveParent = null;
            objectPositionerBehaviour.movingObject = null;
        });
        if (popUpsBehaviour.activateCreatingPopUp)
        {
            popUpsBehaviour.activateCreatingPopUp = false;
            popUpsBehaviour.CreatingPopUp();
        }
        if (popUpsBehaviour.activateMovingPopUp)
        {
            popUpsBehaviour.activateMovingPopUp = false;
            popUpsBehaviour.MovingPopUp();
        }
    }

    //ObjectDeleteBehaviour
    public void DeleteAnimation()
    {
        if (isParent)
        {
            selectedToDeleteParent = selectedToDeleteTemp.transform.parent;
            selectedToDeleteTemp = selectedToDeleteParent.gameObject;
        }
        LeanTween.rotateAround(selectedToDeleteTemp, Vector3.up, selectedToDeleteTemp.transform.rotation.y + 360, 0.8f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(selectedToDeleteTemp, new Vector3 (deleteObjectMaxSize, deleteObjectMaxSize, deleteObjectMaxSize), 0.3f).setEase(LeanTweenType.easeOutQuint).setOnComplete(() =>
        {
            LeanTween.scale(selectedToDeleteTemp, Vector3.zero, 0.4f).setEase(LeanTweenType.easeInQuint).setOnComplete(() =>
            {
                if (isParent)
                {
                    Destroy(selectedToDeleteParent.gameObject);
                }
                else
                {
                    Destroy(selectedToDeleteTemp);
                }
                objectDeleteBehaviour.deleting = true;
            });
        });
    }

    public void AreaShadowAnimation()
    {
        if (showShadowArea)
        {
            shadowNeededScale = new Vector3 (shadowScale, 0.001767956f, shadowScale);
            shadowNeededAnim = shadowAnimAppear;
        }
        else if (!showShadowArea)
        {
            shadowNeededScale = Vector3.zero;
            shadowNeededAnim = shadowAnimDisappear;
        }
        LeanTween.scale(areaShadow, shadowNeededScale, shadowAnimSpeed).setEase(shadowNeededAnim);
    }
}
