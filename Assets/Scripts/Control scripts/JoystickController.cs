using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : BaseInputController
{
    public GameObject touchMarker;

    // Rotation speed types for smooth rotation.
    // Low speed when joystick marker is close to the center of the joystick.
    public float lowSpeed = 100f;

    // Medium speed when joystick marker is not too close and not too far from the center of the joystick.
    public float medSpeed = 400f;

    // High speed when joystick marker is far from the center of the joystick.
    public float highSpeed = 800f;

    // Width of the joystick.
    public float width;

    void Start()
    {
        BaseInitialization();
        BaseJoystickInitialization();
    }

    void Update()
    {
        // Checking for game is unpaused.
        if (!PauseController.IsGamePaused)
        {
            // Checking for device used in the game (Can be directly set from the GameSettings script).
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

    public void BaseJoystickInitialization()
    {
        touchMarker.transform.position = transform.position;
        width = gameObject.GetComponent<RectTransform>().rect.width;
    }

    public virtual void WhatToDoSmartphone()
    {
        switch (smartphoneInput.CheckInputSmartphone())
        {
            case "continious":

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

                touchMarker.transform.position = transform.position;
                targetVector = new Vector3(0, 0, 0);
                plController.rotationSpeed = 0;

                Vector3 touchPosition = smartphoneInput.touch.position;
                plController.targetMove = touchPosition - Camera.main.WorldToScreenPoint(player.transform.position);

                if (tweenController.isThrowTweenCompleted)
                {
                    ThrowSaber(smartphoneInput.touch.position);
                }

                break;


            case "none":

                touchMarker.transform.position = transform.position;
                targetVector = new Vector3(0, 0, 0);
                plController.rotationSpeed = 0;

                break;
        }
    }

    public virtual void WhatToDoPC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseInput.SingleClick();
        }
        if (Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
        {

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
