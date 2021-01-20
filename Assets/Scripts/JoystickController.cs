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
        if (Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
        {
            Vector3 touchPos = Input.mousePosition; //Also change this to Input.GetTouch(0).position
            targetVector = touchPos - transform.position;
            plController.targetMove = targetVector;

            if (targetVector.magnitude < 100)
            {
                touchMarker.transform.position = touchPos;
            }
            else
            {
                float newX = transform.position.x + Mathf.Sin(Mathf.Atan2(targetVector.x, targetVector.y)) * 100;
                float newY = transform.position.y + Mathf.Cos(Mathf.Atan2(targetVector.x, targetVector.y)) * 100;
                touchMarker.transform.position = new Vector3(newX, newY, transform.position.z);
            }
        }
        else
        {
            touchMarker.transform.position = transform.position;
        }
    }
}
