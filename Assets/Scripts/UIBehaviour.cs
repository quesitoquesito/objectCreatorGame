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
    [SerializeField] GameObject showButtonsArea;
    [SerializeField] Button buttonCreateBut;
    [SerializeField] GameObject buttonsBackground;
    [SerializeField] LeanTweenType animUp;
    [SerializeField] LeanTweenType animDown;
    [SerializeField] LeanTweenType buttonBackgroundAnimUp;
    [SerializeField] LeanTweenType buttonBackgroundAnimDown;
    LeanTweenType animVertical;
    [SerializeField] float timeBetweenAnim;
    [SerializeField] float animSpeed;
    bool directionUp;
    public bool create;
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
        buttonCreateBut.interactable = false;
        showButtonsArea.SetActive(false);
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
        LeanTween.moveX(buttonCreate, buttonCreate.transform.position.x, timeBetweenAnim).setOnComplete(() =>
        {
            LeanTween.moveLocalY(buttonCreate, yButtonPos, animSpeed).setEase(animVertical);
            LeanTween.moveX(buttonCreate, buttonCreate.transform.position.x, timeBetweenAnim).setOnComplete(() =>
            {
                LeanTween.moveLocalY(buttonMove, yButtonPos, animSpeed).setEase(animVertical);
            });
            LeanTween.moveX(buttonCreate, buttonCreate.transform.position.x, timeBetweenAnim * 2).setOnComplete(() =>
            {
                LeanTween.moveLocalY(buttonRotate, yButtonPos, animSpeed).setEase(animVertical);
            });
            LeanTween.moveX(buttonCreate, buttonCreate.transform.position.x, timeBetweenAnim * 3).setOnComplete(() =>
            {
                LeanTween.moveX(buttonCreate, buttonCreate.transform.position.x, timeBetweenAnim).setOnComplete(() =>
                {
                    LeanTween.moveLocalY(buttonsBackground, yButBackgroundPos, animSpeed).setEase(buttonBackgroundAnimDown); //Animación del fondo de botones.
                });
                LeanTween.moveLocalY(buttonDelete, yButtonPos, animSpeed).setEase(animVertical).setOnComplete(() =>
                {
                    buttonCreateBut.interactable = true;
                    if (create)
                    {
                        create = false;
                        showButtonsArea.SetActive(false);
                        sideBarObj.GetComponent<UISideBarBehaviour>().ShowSlider();
                    }
                    else if (!create)
                    {
                        showButtonsArea.SetActive(true);
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
