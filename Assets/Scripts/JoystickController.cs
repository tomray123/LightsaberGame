using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public GameObject touchMarker;

    public Vector3 targetVector;

    public PlayerController plController;

    public float lowSpeed = 100f;

    public float medSpeed = 400f;

    public float highSpeed = 800f;

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

                if (targetVector.magnitude < 40)
                {
                    plController.rotationSpeed = medSpeed;
                }
                /*else if(targetVector.magnitude < 40)
                {
                    plController.rotationSpeed = medSpeed;
                }*/
                else
                {
                    plController.rotationSpeed = highSpeed;
                }
            }
            else
            {
                plController.rotationSpeed = highSpeed;
                float newX = transform.position.x + Mathf.Sin(Mathf.Atan2(targetVector.x, targetVector.y)) * 100;
                float newY = transform.position.y + Mathf.Cos(Mathf.Atan2(targetVector.x, targetVector.y)) * 100;
                touchMarker.transform.position = new Vector3(newX, newY, transform.position.z);
            }
        }
        else
        {
            touchMarker.transform.position = transform.position;
            targetVector = new Vector3(0, 0, 0);
            plController.rotationSpeed = 0;
        }
    }
}
