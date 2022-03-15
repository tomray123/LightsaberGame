using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooker : BaseInputController
{
    // Rotation speed.
    public float speed = 800f;

    void Start()
    {
        BaseInitialization();
    }

    void Update()
    {
        // Checking for game is unpaused.
        if (!PauseController.IsGamePaused)
        {
            // Checking for device used in the game (Can be directly set from the GameSettings script).
            switch (gameSettings.TargetDevice)
            {
                case Device.PC:

                    WhatToDoPC();

                    break;

                case Device.Smartphone:

                    WhatToDoSmartphone();

                    break;
            }
        }
    }

    // Defines joystick actions based on user actions on smartphone.
    public void WhatToDoSmartphone()
    {
        // Which action was made by user.
        switch (smartphoneInput.CheckInputSmartphone())
        {
            // Holding a finger on a screen.
            case "continious":

                Vector3 tapPosition = Input.GetTouch(0).position;
                // Calculating player rotation vector.
                var dir = tapPosition - Camera.main.WorldToScreenPoint(player.transform.position);
                plController.targetMove = dir;
                plController.rotationSpeed = speed;

                break;

            case "doubleTap":

                Vector3 touchPosition = smartphoneInput.touch.position;
                // Rotating player towards the saber throw.
                plController.targetMove = touchPosition - Camera.main.WorldToScreenPoint(player.transform.position);

                // Throwing the saber.
                if (tweenController.isThrowTweenCompleted)
                {
                    ThrowSaber(smartphoneInput.touch.position);
                }

                break;
        }
    }

    // Defines joystick actions based on user actions on PC.
    public void WhatToDoPC()
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
            // Calculating player rotation vector.
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
            plController.targetMove = dir;
            plController.rotationSpeed = speed;
        }

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
