using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationsBehaviour : MonoBehaviour
{
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

    //ObjectDeleteBehaviour
    [SerializeField] ObjectDeleteBehaviour objectDeleteBehaviour;
    [HideInInspector] public GameObject selectedToDeleteTemp;

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
                    uiBehaviour.buttonCreateBut.interactable = true;
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
                //Comprueba si el slider est� subiendo para activar el bot�n (invisible) para los botones del men�.
                if (uiSideBarBehaviour.sliderActive)
                {
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
                objectPositionerBehaviour.movingObject.transform.LeanScale(new Vector3(1, 1, 1), popAnimDuration).setEase(LeanTweenType.easeOutBack);
                objectPositionerBehaviour.isObjectMoving = true;
            });
        });
    }
    public void SetObjectAnimation()
    {
        LeanTween.scale(objectPositionerBehaviour.movingObject, new Vector3(1.3f, 1.3f, 1.3f), 0.5f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.rotateAround(objectPositionerBehaviour.movingObject, Vector3.up, objectPositionerBehaviour.movingObject.transform.rotation.y - 360, 1f).setEase(LeanTweenType.easeInOutBack).setOnComplete(() =>
        {
            LeanTween.scale(objectPositionerBehaviour.movingObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutBack);
            objectPositionerBehaviour.movingObject.transform.Rotate(Vector3.zero);
        });
    }

    //ObjectDeleteBehaviour
    public void DeleteAnimation()
    {
        LeanTween.rotateAround(selectedToDeleteTemp, Vector3.up, selectedToDeleteTemp.transform.rotation.y + 360, 0.8f).setEase(LeanTweenType.easeInBack);
        LeanTween.scale(selectedToDeleteTemp, new Vector3 (1.1f, 1.1f, 1.1f), 0.3f).setEase(LeanTweenType.easeOutQuint).setOnComplete(() =>
        {
            LeanTween.scale(selectedToDeleteTemp, Vector3.zero, 0.4f).setEase(LeanTweenType.easeInQuint).setOnComplete(() =>
            {
                Destroy(selectedToDeleteTemp);
                objectDeleteBehaviour.deleting = true;
            });
        });
    }
}