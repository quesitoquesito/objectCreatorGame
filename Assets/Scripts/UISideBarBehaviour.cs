using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UISideBarBehaviour : MonoBehaviour
{
    [SerializeField] PopUpsBehaviour popUpsBehaviour;
    [SerializeField] AnimationsBehaviour animationsBehaviour;

    public float heightInSight;
    public float heightOutOfSight;
    [SerializeField] Button closeSlider;
    public GameObject closeSliderObj;
    public Button areaButton;
    [HideInInspector] public float heightNeeded;
    [HideInInspector] public bool sliderActive;
    [SerializeField] float mouseJumpAmount;
    float scrollPos;
    public Button[] sliderButtons;

    [SerializeField] float minHeightInSight;
    [SerializeField] float maxHeightInSight;

    private void Start()
    {
        sliderActive = false;
        closeSliderObj.SetActive(false);
        for (int i = 0; i < sliderButtons.Length; i++)
        {
            sliderButtons[i].interactable = false;
        }
    }
    private void Update()
    {
        //Si el slider está activo permite usar el scroll para navegar entre los objetos, también marca los límites de altura para no hacer desaparecer el slider.
        if (sliderActive)
        {
            scrollPos = Mathf.Round(gameObject.transform.localPosition.y + (-Input.mouseScrollDelta.y * mouseJumpAmount));
            if (scrollPos < maxHeightInSight)
            {
                scrollPos = maxHeightInSight;
            }
            if (scrollPos > minHeightInSight)
            {
                scrollPos = minHeightInSight;
            }
            transform.localPosition = new Vector3(transform.localPosition.x, scrollPos, transform.localPosition.z);
        }
    }
    public void ShowSlider()
    {
        if (!sliderActive)
        {
            areaButton.interactable = false;
            heightNeeded = heightInSight;
            animationsBehaviour.sliderAnim = animationsBehaviour.sliderAnimTypeDown;
            animationsBehaviour.sliderAnimDurationActual = animationsBehaviour.sliderAnimDuration;
        }
        else if (sliderActive)
        {
            closeSliderObj.SetActive(false);
            heightNeeded = heightOutOfSight;
            animationsBehaviour.sliderAnim = animationsBehaviour.sliderAnimTypeUp;
            animationsBehaviour.sliderAnimDurationActual = ((heightOutOfSight - gameObject.transform.position.y) * animationsBehaviour.sliderAnimDuration) / heightOutOfSight;
        }
        animationsBehaviour.ShowSliderAnimation();
    }

    public void HideSlider()
    {
        popUpsBehaviour.activateCreatingPopUp = false;
        popUpsBehaviour.CreatingPopUp();
        ShowSlider();
    }

}
