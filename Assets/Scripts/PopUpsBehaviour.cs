using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpsBehaviour : MonoBehaviour
{
    [SerializeField] float animDuration;
    [SerializeField] float animCenterDuration;
    [SerializeField] float centerAnimDelay;
    [HideInInspector] LeanTweenType animWanted;
    float heightWanted;

    [SerializeField] GameObject createPopUp;
    [SerializeField] GameObject movePopUp;
    [SerializeField] GameObject rotatePopUp;
    [SerializeField] GameObject deletePopUp;
    [SerializeField] GameObject scalePopUp;

    [SerializeField] LeanTweenType PopUpCenter;
    [SerializeField] LeanTweenType PopUpDown;
    [SerializeField] LeanTweenType PopUpUp;
    [SerializeField] float maxHeight;
    [SerializeField] float minHeight;

    [HideInInspector] public bool activateCreatingPopUp;
    [HideInInspector] public bool activateMovingPopUp;
    [HideInInspector] public bool activateRotatingPopUp;
    [HideInInspector] public bool activateDeletingPopUp;
    [HideInInspector] public bool activateScalingPopUp;

    [HideInInspector] public bool center;


    public void CenterCreatePopUp()
    {
        if (center)
        {
            LeanTween.moveX(createPopUp, createPopUp.transform.position.x, centerAnimDelay).setOnComplete(() =>
            {
                LeanTween.moveLocalX(createPopUp, 0, animCenterDuration).setEase(PopUpCenter);
            });
        }
    }

    public void CreatingPopUp()
    {
        if (activateCreatingPopUp)
        {
            heightWanted = minHeight;
            animWanted = PopUpDown;
        }
        else
        {
            heightWanted = maxHeight;
            animWanted = PopUpUp;
        }
        LeanTween.moveLocalY(createPopUp, heightWanted, animDuration).setEase(animWanted).setOnComplete(() =>
        {
            if (!activateCreatingPopUp && center)
            {
                createPopUp.transform.localPosition = (new Vector3(-154, createPopUp.transform.position.y, createPopUp.transform.position.z));
            }
        });
    }

    public void MovingPopUp()
    {
        if (activateMovingPopUp)
        {
            heightWanted = minHeight;
            animWanted = PopUpDown;
        }
        else
        {
            heightWanted = maxHeight;
            animWanted = PopUpUp;
        }
        LeanTween.moveLocalY(movePopUp, heightWanted, animDuration).setEase(animWanted);
    }

    public void RotatingPopUp()
    {
        if (activateRotatingPopUp)
        {
            heightWanted = minHeight;
            animWanted = PopUpDown;
        }
        else
        {
            heightWanted = maxHeight;
            animWanted = PopUpUp;
        }
        LeanTween.moveLocalY(rotatePopUp, heightWanted, animDuration).setEase(animWanted);
    }

    public void DeletingPopUp()
    {
        if (activateDeletingPopUp)
        {
            heightWanted = minHeight;
            animWanted = PopUpDown;
        }
        else
        {
            heightWanted = maxHeight;
            animWanted = PopUpUp;
        }
        LeanTween.moveLocalY(deletePopUp, heightWanted, animDuration).setEase(animWanted);
    }

    public void ScalingPopUp()
    {
        if (activateScalingPopUp)
        {
            heightWanted = minHeight;
            animWanted = PopUpDown;
        }
        else
        {
            heightWanted = maxHeight;
            animWanted = PopUpUp;
        }
        LeanTween.moveLocalY(scalePopUp, heightWanted, animDuration).setEase(animWanted);
    }
}
