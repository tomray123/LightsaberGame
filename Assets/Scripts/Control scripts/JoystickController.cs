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

    // Defines joystick actions based on user actions on smartphone.
    public virtual void WhatToDoSmartphone()
    {
        // Which action was made by user.
        switch (smartphoneInput.CheckInputSmartphone())
        {
            // Holding a finger on a screen.
            case "continious":

                Vector3 localTouchPos = Input.GetTouch(0).position;
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(localTouchPos);
                Vector3 localPos = Camera.main.WorldToScreenPoint(transform.position);

                touchPos.z = transform.position.z;
                localTouchPos.z = localPos.z;

                // Calculating player rotation vector.
                targetVector = touchPos - transform.position;
                plController.targetMove = targetVector;

                Vector3 localTargetPos = localTouchPos - localPos;

                // If player touch is within joystick area.
                if (localTargetPos.magnitude < width / 2)
                {
                    // Setting touch marker position to touch position.
                    touchMarker.transform.position = touchPos;

                    // Checking for distance between touch position and joystick's center 
                    // and setting different speed depends on this distance.
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
                // If player touch is somewhere outside of joystick area.
                else
                {
                    plController.rotationSpeed = highSpeed;
                    // Defining coordinates for touch marker within joystick area.
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
                // Rotating player towards the saber throw.
                plController.targetMove = touchPosition - Camera.main.WorldToScreenPoint(player.transform.position);

                // Throwing the saber.
                if (tweenController.isThrowTweenCompleted)
                {
                    ThrowSaber(smartphoneInput.touch.position);
                }

                break;

            // If player isn't touching a screen.
            case "none":

                // Do nothing.
                touchMarker.transform.position = transform.position;
                targetVector = new Vector3(0, 0, 0);
                plController.rotationSpeed = 0;

                break;
        }
    }

    // Defines joystick actions based on user actions on PC.
    public virtual void WhatToDoPC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Telling everybody about single click.
            mouseInput.SingleClick();
            mouseInput.onScreenClick.Invoke();
        }

        // If player is holding left mouse button.
        if (Input.GetMouseButton(0)) 
        {
            Vector3 localTouchPos = Input.mousePosition;
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(localTouchPos);
            Vector3 localPos = Camera.main.WorldToScreenPoint(transform.position);

            touchPos.z = transform.position.z;
            localTouchPos.z = localPos.z;

            // Calculating player rotation vector.
            targetVector = touchPos - transform.position;
            plController.targetMove = targetVector;

            Vector3 localTargetPos = localTouchPos - localPos;

            // If player's click is within joystick area.
            if (localTargetPos.magnitude < width / 2)
            {
                // Setting touch marker position to click position.
                touchMarker.transform.position = touchPos;

                // Checking for distance between touch position and joystick's center 
                // and setting different speed depends on this distance.
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
            // If player's click is somewhere outside of joystick area.
            else
            {
                plController.rotationSpeed = highSpeed;
                // Defining coordinates for touch marker within joystick area.
                float newX = localPos.x + Mathf.Sin(Mathf.Atan2(localTargetPos.x, localTargetPos.y)) * width / 2;
                float newY = localPos.y + Mathf.Cos(Mathf.Atan2(localTargetPos.x, localTargetPos.y)) * width / 2;
                touchMarker.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(newX, newY, transform.position.z));
            }
        }
        // If player isn't clicking on left mouse button.
        else
        {
            // Do nothing.
            touchMarker.transform.position = transform.position;
            targetVector = new Vector3(0, 0, 0);
            plController.rotationSpeed = 0;
        }

        // If player clicked on right mouse button.
        if (Input.GetMouseButtonDown(1))
        {
            // Rotating player towards the saber throw.
            plController.targetMove = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
            // Throwing the saber.
            if (tweenController.isThrowTweenCompleted)
            {
                ThrowSaber(Input.mousePosition);
            }
        }
    }
}
