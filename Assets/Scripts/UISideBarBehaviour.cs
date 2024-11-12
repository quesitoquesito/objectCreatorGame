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
    [SerializeField] Button closeSlider;
    [SerializeField] GameObject closeSliderObj;
    [SerializeField] GameObject areaButton;
    [SerializeField] LeanTweenType sliderAnimTypeUp;
    [SerializeField] LeanTweenType sliderAnimTypeDown;
    LeanTweenType sliderAnim;
    [SerializeField] float appearDelay;
    float heightNeeded;
    bool sliderActive;

    private void Start()
    {
        sliderActive = false;
        closeSliderObj.SetActive(false);
    }
    public void ShowSlider()
    {
        if (!sliderActive)
        {
            areaButton.SetActive(false);
            heightNeeded = heightInSight;
            sliderAnim = sliderAnimTypeDown;
        }
        else if (sliderActive)
        {
            closeSliderObj.SetActive(false);
            heightNeeded = heightOutOfSight;
            sliderAnim = sliderAnimTypeUp;
        }
        LeanTween.moveLocalY(gameObject, gameObject.transform.position.y, appearDelay).setOnComplete(() =>
        {
            LeanTween.moveLocalY(gameObject, heightNeeded, sliderAnimDuration).setEase(sliderAnim).setOnComplete(() =>
            {
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
        sliderActive = true;
        ShowSlider();
    }
}
