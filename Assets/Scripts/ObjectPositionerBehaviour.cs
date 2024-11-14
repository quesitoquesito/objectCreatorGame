using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionerBehaviour : MonoBehaviour
{
    SliderButtonsBehaviour sliderBehaviour;
    public bool movingObject;
    private void Start()
    {
        movingObject = false;
    }
    void Update()
    {
        if (movingObject)
        {
            sliderBehaviour.objectCreated.SetActive(false);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
                sliderBehaviour.objectCreated.transform.position = hitPoint;
            }
            sliderBehaviour.objectCreated.SetActive(true);
        }
    }
}
