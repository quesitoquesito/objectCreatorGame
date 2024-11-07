using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] GameObject buttonCreate;
    [SerializeField] GameObject buttonMove;
    [SerializeField] GameObject buttonRotate;
    [SerializeField] GameObject buttonDelete;
    [SerializeField] Button showButtonsArea;
    [SerializeField] LeanTweenType animUp;
    [SerializeField] LeanTweenType animDown;
    LeanTweenType animVertical;
    [SerializeField] float timeBetweenAnim;
    [SerializeField] float animSpeed;
    bool directionUp;
    float yButtonPos;


    private void Start()
    {
        directionUp = true;
    }
    public void ShowOptionsMenu() 
    {
        if (directionUp)
        {
            directionUp = false;
            yButtonPos = -428;
            animVertical = animUp;
        }
        else if (!directionUp)
        {
            directionUp = true;
            yButtonPos = -649;
            animVertical = animDown;
        }
        showButtonsArea.interactable = false;
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
            LeanTween.moveLocalY(buttonDelete, yButtonPos, animSpeed).setEase(animVertical).setOnComplete(() =>
            {
                showButtonsArea.interactable = true;
            });
        });
    }
    void Create() 
    {
        
    }
}
