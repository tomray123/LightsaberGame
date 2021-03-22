using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : InputController
{
    void Start()
    {
        BaseJoystickInitialization();
    }

    void Update()
    {
        switch (GameSettings.device)
        {
            case GameSettings.Device.PC:

                WhatToDoPC();

                break;

            case GameSettings.Device.Smartphone:

                WhatToDoSmartphone();

                break;

        }
    }

    public void WhatToDoSmartphone()
    {
        switch (CheckInputSmartphone())
        {
            case "continious":

                plController.isPlayerMoving = true;

                Vector3 localTouchPos = Input.GetTouch(0).position; //Also change this to Input.GetTouch(0).position
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(localTouchPos);
                Vector3 localPos = Camera.main.WorldToScreenPoint(transform.position);

                touchPos.z = transform.position.z;
                localTouchPos.z = localPos.z;

                targetVector = touchPos - transform.position;
                plController.targetMove = targetVector;

                Vector3 localTargetPos = localTouchPos - localPos;

                if (localTargetPos.magnitude < width / 2)
                {

                    touchMarker.transform.position = touchPos;

                    if (localTargetPos.magnitude < 0.1f * width / 2)
                    {
                        plController.rotationSpeed = lowSpeed;
                    }
                    else if (localTargetPos.magnitude < 0.3f * width / 2)
                    {
                        plController.rotationSpeed = medSpeed;
                    }
                    else
                    {
                        plController.rotationSpeed = highSpeed;
                    }
                }
                else
                {
                    plController.rotationSpeed = highSpeed;
                    float newX = localPos.x + Mathf.Sin(Mathf.Atan2(localTargetPos.x, localTargetPos.y)) * width / 2;
                    float newY = localPos.y + Mathf.Cos(Mathf.Atan2(localTargetPos.x, localTargetPos.y)) * width / 2;
                    touchMarker.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(newX, newY, transform.position.z));
                }

                break;

            case "doubleTap":

                plController.isPlayerMoving = false;
                touchMarker.transform.position = transform.position;
                targetVector = new Vector3(0, 0, 0);
                plController.rotationSpeed = 0;

                Vector3 touchPosition = touch.position;
                plController.targetMove = touchPosition - Camera.main.WorldToScreenPoint(player.transform.position);

                if (tweenController.isThrowTweenCompleted)
                {
                    ThrowSaber(touch.position);
                }

                break;


            case "none":

                plController.isPlayerMoving = false;
                touchMarker.transform.position = transform.position;
                targetVector = new Vector3(0, 0, 0);
                plController.rotationSpeed = 0;

                break;
        }
    }

    public void WhatToDoPC()
    {
        if (Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
        {
            plController.isPlayerMoving = true;

            Vector3 localTouchPos = Input.mousePosition; //Also change this to Input.GetTouch(0).position
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(localTouchPos);
            Vector3 localPos = Camera.main.WorldToScreenPoint(transform.position);

            touchPos.z = transform.position.z;
            localTouchPos.z = localPos.z;

            targetVector = touchPos - transform.position;
            plController.targetMove = targetVector;

            Vector3 localTargetPos = localTouchPos - localPos;

            if (localTargetPos.magnitude < width / 2)
            {

                touchMarker.transform.position = touchPos;

                if (localTargetPos.magnitude < 0.1f * width / 2)
                {
                    plController.rotationSpeed = lowSpeed;
                }
                else if (localTargetPos.magnitude < 0.3f * width / 2)
                {
                    plController.rotationSpeed = medSpeed;
                }
                else
                {
                    plController.rotationSpeed = highSpeed;
                }
            }
            else
            {
                plController.rotationSpeed = highSpeed;
                float newX = localPos.x + Mathf.Sin(Mathf.Atan2(localTargetPos.x, localTargetPos.y)) * width / 2;
                float newY = localPos.y + Mathf.Cos(Mathf.Atan2(localTargetPos.x, localTargetPos.y)) * width / 2;
                touchMarker.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(newX, newY, transform.position.z));
            }
        }
        else
        {
            plController.isPlayerMoving = false;
            touchMarker.transform.position = transform.position;
            targetVector = new Vector3(0, 0, 0);
            plController.rotationSpeed = 0;
        }

        if (Input.GetMouseButtonDown(1)) //Change this to (Input.touchCount > 1) in order to switch PC to mobile
        {
            plController.targetMove = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
            if (tweenController.isThrowTweenCompleted)
            {
                ThrowSaber(Input.mousePosition);
            }
        }
    }
}
