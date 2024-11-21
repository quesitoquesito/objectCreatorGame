using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] PopUpsBehaviour popUpsBehaviour;
    [SerializeField] AnimationsBehaviour animationsBehaviour;
    public GameObject sideBarObj;
    public GameObject buttonCreate;
    public GameObject buttonMove;
    public GameObject buttonRotate;
    public GameObject buttonDelete;
    public GameObject showButtonsObj;
    public Button showButtonsArea;
    public Button[] menuButtons;
    public GameObject buttonsBackground;
    bool directionUp;
    [HideInInspector] public bool create;
    public float yButtonPos;
    public float yButBackgroundPos;
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
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].interactable = false;
        }
        showButtonsArea.interactable = false;
        if (directionUp)
        {
            directionUp = false;
            yButtonPos = maxHeightButtons; //Por defecto -428
            yButBackgroundPos = maxHeightBackground; //Por defecto -500
            animationsBehaviour.animVertical = animationsBehaviour.animUp;
            LeanTween.moveLocalY(buttonsBackground, yButBackgroundPos, animationsBehaviour.animSpeed).setEase(animationsBehaviour.buttonBackgroundAnimUp); //Animación del fondo de botones.
        }
        else if (!directionUp)
        {
            directionUp = true;
            yButtonPos = minHeightButtons; //Por defecto -649
            yButBackgroundPos = minHeightBackground; //Por defecto -750
            animationsBehaviour.animVertical = animationsBehaviour.animDown;
        }
        animationsBehaviour.ShowOptionsMenuAnimation();
    }
    public void Create() 
    {
        create = true;
        popUpsBehaviour.activateCreatingPopUp = true;
        popUpsBehaviour.CreatingPopUp();
        ShowOptionsMenu();
    }
}
