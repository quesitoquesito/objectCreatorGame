using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UISideBarBehaviour : MonoBehaviour
{
    [SerializeField] AnimationsBehaviour animationsBehaviour;

    public float heightInSight;
    public float heightOutOfSight;
    [SerializeField] Button closeSlider; //revisar uso, si se intercambia area por botón
    public GameObject closeSliderObj;
    public Button areaButton;
    public float heightNeeded;
    public bool sliderActive;
    [SerializeField] float mouseJumpAmount;
    float scrollPos;
    public Button[] sliderButtons;

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
        ShowSlider();
    }

}
