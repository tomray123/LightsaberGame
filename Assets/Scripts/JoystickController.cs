using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public GameObject touchMarker;

    Vector3 targetVector;

    public PlayerController plController;

    void Start()
    {
        touchMarker.transform.position = transform.position;
    }

    void Update()
    {
        if(Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
        {
            Vector3 touchPos = Input.mousePosition; //Also change this to Input.GetTouch(0).position
            targetVector = touchPos - transform.position;

            if(targetVector.magnitude < 100)
            {
                touchMarker.transform.position = touchPos;
                plController.targetMove = targetVector;
            }
        else
        {
            touchMarker.transform.position = transform.position;
            //plController.targetMove = new Vector3(0, 0, 0);
        }
        }
    }
}
