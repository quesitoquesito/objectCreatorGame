using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISideBarBehaviour : MonoBehaviour
{
    //Animación slider: lista cae desde arriba y baja hasta llegar al primer elemento, con rueda dl ratón se puede bajar para ver todos los items
    [SerializeField] GameObject slider;
    [SerializeField] float min
    

    public void ShowSlider()
    {
        LeanTween.MoveLocalY(gameObject,)
    }
}
