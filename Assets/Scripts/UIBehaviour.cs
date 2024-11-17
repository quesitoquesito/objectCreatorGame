using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] AnimationsBehaviour animationsBehaviour;
    public GameObject sideBarObj; //acceder desde script de animations a sidebar
    public GameObject buttonCreate;
    public GameObject buttonMove;
    public GameObject buttonRotate;
    public GameObject buttonDelete;
    public GameObject showButtonsObj;
    public Button showButtonsArea;
    public Button buttonCreateBut;
    public GameObject buttonsBackground;
    bool directionUp;
    public bool create;
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
        buttonCreateBut.interactable = false;
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
        ShowOptionsMenu();
    }
}
