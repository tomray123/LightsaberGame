using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatJoystickController : JoystickController
{
    private bool isMouseHeld = false;

    void Start()
    {
        BaseJoystickInitialization();
        GetComponent<Image>().enabled = false;
        touchMarker.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        if (!PauseController.IsGamePaused)
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
    }

    public override void WhatToDoSmartphone()
    {
        switch (CheckInputSmartphone())
        {
            case "continious":

                Vector3 localTouchPos = Input.GetTouch(0).position; //Also change this to Input.GetTouch(0).position
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(localTouchPos);
                Vector3 localPos = Camera.main.WorldToScreenPoint(transform.position);

                touchPos.z = transform.position.z;
                localTouchPos.z = localPos.z;

                if (isMouseHeld == false)
                {

                    GetComponent<Image>().enabled = true;
                    touchMarker.GetComponent<Image>().enabled = true;
                    transform.position = touchPos;
                    isMouseHeld = true;
                }
                else
                {
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

                break;

            case "doubleTap":

                GetComponent<Image>().enabled = false;
                touchMarker.GetComponent<Image>().enabled = false;
                isMouseHeld = false;
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

                GetComponent<Image>().enabled = false;
                touchMarker.GetComponent<Image>().enabled = false;
                isMouseHeld = false;
                touchMarker.transform.position = transform.position;
                targetVector = new Vector3(0, 0, 0);
                plController.rotationSpeed = 0;

                break;
        }
    }

    public override void WhatToDoPC()
    {
        if (Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
        {

            Vector3 localTouchPos = Input.mousePosition; //Also change this to Input.GetTouch(0).position
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(localTouchPos);
            Vector3 localPos = Camera.main.WorldToScreenPoint(transform.position);

            touchPos.z = transform.position.z;
            localTouchPos.z = localPos.z;

            if (isMouseHeld == false)
            {

                GetComponent<Image>().enabled = true;
                touchMarker.GetComponent<Image>().enabled = true;
                transform.position = touchPos;
                isMouseHeld = true;
            }
            else
            {
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

        }
        else
        {
            GetComponent<Image>().enabled = false;
            touchMarker.GetComponent<Image>().enabled = false;
            isMouseHeld = false;
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
