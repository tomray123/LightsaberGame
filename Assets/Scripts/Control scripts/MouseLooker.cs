using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooker : BaseInputController
{
    public float speed = 800f;

    // Start is called before the first frame update
    void Start()
    {
        BaseInitialization();
    }

    // Update is called once per frame
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

    public void WhatToDoSmartphone()
    {
        switch (smartphoneInput.CheckInputSmartphone())
        {
            case "continious":

                Vector3 tapPosition = Input.GetTouch(0).position;
                var dir = tapPosition - Camera.main.WorldToScreenPoint(player.transform.position);
                plController.targetMove = dir;
                plController.rotationSpeed = speed;

                break;

            case "doubleTap":

                Vector3 touchPosition = smartphoneInput.touch.position;
                plController.targetMove = touchPosition - Camera.main.WorldToScreenPoint(player.transform.position);

                if (tweenController.isThrowTweenCompleted)
                {
                    ThrowSaber(smartphoneInput.touch.position);
                }

                break;
        }
    }

    public void WhatToDoPC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseInput.SingleClick();
        }
        if (Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
        {
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
            plController.targetMove = dir;
            plController.rotationSpeed = speed;
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
