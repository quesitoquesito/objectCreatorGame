using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] GameObject sideBarObj;
    [SerializeField] GameObject buttonCreate;
    [SerializeField] GameObject buttonMove;
    [SerializeField] GameObject buttonRotate;
    [SerializeField] GameObject buttonDelete;
    [SerializeField] Button showButtonsArea;
    [SerializeField] GameObject buttonsBackground;
    [SerializeField] LeanTweenType animUp;
    [SerializeField] LeanTweenType animDown;
    [SerializeField] LeanTweenType buttonBackgroundAnimUp;
    [SerializeField] LeanTweenType buttonBackgroundAnimDown;
    LeanTweenType animVertical;
    [SerializeField] float timeBetweenAnim;
    [SerializeField] float animSpeed;
    bool directionUp;
    bool create;
    float yButtonPos;
    float yButBackgroundPos;
    [SerializeField] float maxHeightButtons;
    [SerializeField] float minHeightButtons;
    [SerializeField] float maxHeightBackground;
    [SerializeField] float minHeightBackground;


    private void Start()
    {
        directionUp = true;
        create = false;
    }
    public void ShowOptionsMenu() 
    {
        if (directionUp)
        {
            directionUp = false;
            yButtonPos = maxHeightButtons; //Por defecto -428
            yButBackgroundPos = maxHeightBackground; //Por defecto -500
            animVertical = animUp;
            LeanTween.moveLocalY(buttonsBackground, yButBackgroundPos, animSpeed).setEase(buttonBackgroundAnimUp); //Animación del fondo de botones.
        }
        else if (!directionUp)
        {
            directionUp = true;
            yButtonPos = minHeightButtons; //Por defecto -649
            yButBackgroundPos = minHeightBackground; //Por defecto -750
            animVertical = animDown;
        }
        showButtonsArea.interactable = false;
        LeanTween.moveLocalX(buttonCreate, -699f, timeBetweenAnim).setOnComplete(() =>
        {
            LeanTween.moveLocalY(buttonCreate, yButtonPos, animSpeed).setEase(animVertical);
            LeanTween.moveLocalX(buttonCreate, -699f, timeBetweenAnim).setOnComplete(() =>
            {
                LeanTween.moveLocalY(buttonMove, yButtonPos, animSpeed).setEase(animVertical);
            });
            LeanTween.moveLocalX(buttonCreate, -699f, timeBetweenAnim * 2).setOnComplete(() =>
            {
                LeanTween.moveLocalY(buttonRotate, yButtonPos, animSpeed).setEase(animVertical);
            });
            LeanTween.moveLocalX(buttonCreate, -699f, timeBetweenAnim * 3).setOnComplete(() =>
            {
                LeanTween.moveLocalX(buttonCreate, -699f, timeBetweenAnim).setOnComplete(() =>
                {
                    LeanTween.moveLocalY(buttonsBackground, yButBackgroundPos, animSpeed).setEase(buttonBackgroundAnimDown).setOnComplete(() => //Animación del fondo de botones.
                    {
                        showButtonsArea.interactable = true;
                    });
                });
                LeanTween.moveLocalY(buttonDelete, yButtonPos, animSpeed).setEase(animVertical).setOnComplete(() =>
                {
                    if (create)
                    {
                        create = false;
                        sideBarObj.GetComponent<UISideBarBehaviour>().ShowSlider();
                    }
                });
            });
        });
    }
    public void Create() 
    {
        create = true;
        ShowOptionsMenu();
    }
}
