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
    [HideInInspector] public LeanTweenType animVertical;
    public float timeBetweenAnim;
    public float animSpeed;

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

    //ObjectDeleteBehaviour
    [SerializeField] ObjectDeleteBehaviour objectDeleteBehaviour;
    [HideInInspector] public GameObject selectedToDeleteTemp;
    [SerializeField] float deleteObjectMaxSize;
    [HideInInspector] public bool isParent;
    Transform selectedToDeleteParent;

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
                    LeanTween.moveLocalY(uiBehaviour.buttonsBackground, uiBehaviour.yButBackgroundPos, animSpeed).setEase(buttonBackgroundAnimDown); //Animación del fondo de botones.
                });
                LeanTween.moveLocalY(uiBehaviour.buttonDelete, uiBehaviour.yButtonPos, animSpeed).setEase(animVertical).setOnComplete(() =>
                {
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
                //Comprueba si el slider está subiendo para activar el botón para los botones del menú.
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
                //Comprueba si el slider está bajando y activa el botón (invisible) para cerrarlo tras terminar la animación, también deja un delay para cerrar el panel.
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
        LeanTween.scale(selectedToMove, new Vector3(setObjectMaxSize, setObjectMaxSize, setObjectMaxSize), 0.5f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.rotateAround(selectedToMove, Vector3.up, selectedToMove.transform.rotation.y - 360, 1f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.moveX(selectedToMove, selectedToMove.transform.position.x, 0.5f).setOnComplete(() =>
        {
            LeanTween.scale(selectedToMove, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutBack);
            selectedToMove.transform.Rotate(Vector3.zero);
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
}
