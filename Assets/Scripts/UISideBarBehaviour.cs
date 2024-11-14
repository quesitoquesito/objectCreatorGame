using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISideBarBehaviour : MonoBehaviour
{
    //Animación slider: lista cae desde arriba y baja hasta llegar al primer elemento, con rueda dl ratón se puede bajar para ver todos los items
    [SerializeField] float heightInSight;
    [SerializeField] float heightOutOfSight;
    [SerializeField] float sliderAnimDuration;
    float sliderAnimDurationActual;
    [SerializeField] Button closeSlider;
    [SerializeField] GameObject closeSliderObj;
    [SerializeField] GameObject areaButton;
    [SerializeField] LeanTweenType sliderAnimTypeUp;
    [SerializeField] LeanTweenType sliderAnimTypeDown;
    LeanTweenType sliderAnim;
    [SerializeField] float appearDelay;
    float heightNeeded;
    bool sliderActive;
    [SerializeField] float mouseJumpAmount;
    float scrollPos;

    private void Start()
    {
        sliderActive = false;
        closeSliderObj.SetActive(false);
    }
    private void Update()
    {
        if (sliderActive)
        {
            scrollPos = gameObject.transform.position.y + (-Input.mouseScrollDelta.y * mouseJumpAmount);
            transform.position = new Vector3(transform.position.x, scrollPos, transform.position.z);
        }
    }
    public void ShowSlider()
    {
        if (!sliderActive)
        {
            areaButton.SetActive(false);
            heightNeeded = heightInSight;
            sliderAnim = sliderAnimTypeDown;
            sliderAnimDurationActual = sliderAnimDuration;
        }
        else if (sliderActive)
        {
            closeSliderObj.SetActive(false);
            heightNeeded = heightOutOfSight;
            sliderAnim = sliderAnimTypeUp;
            sliderAnimDurationActual = ((heightOutOfSight - gameObject.transform.position.y) * sliderAnimDuration) / heightOutOfSight;
        }
        LeanTween.moveY(gameObject, gameObject.transform.position.y, appearDelay).setOnComplete(() =>
        {
            LeanTween.moveLocalY(gameObject, heightNeeded, sliderAnimDurationActual).setEase(sliderAnim).setOnComplete(() =>
            {
                Debug.Log(sliderAnimDurationActual);
                //Comprueba si el slider está subiendo para activar el botón (invisible) para los botones del menú.
                if (sliderActive)
                {
                    areaButton.SetActive(true);
                    sliderActive = false;
                }
                //Comprueba si el slider está bajando y activa el botón (invisible) para cerrarlo tras terminar la animación, también deja un delay para cerrar el panel.
                else if (!sliderActive)
                {
                    LeanTween.moveX(gameObject, gameObject.transform.position.x, appearDelay).setOnComplete(() =>
                    {
                        closeSliderObj.SetActive(true);
                        sliderActive = true;
                    });
                }
            });
        });
    }

    public void HideSlider()
    {
        ShowSlider();
    }

}
