using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SmartphoneInputController : MonoBehaviour
{
    public Touch touch;

    public float touchDuration;

    // Singleton instance.
    public static SmartphoneInputController instance;

    public Action OnSingleTap;

    public UnityEvent onScreenTouch;

    public void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }
    }

    // Calls the OnSingleTap action when single tap.
    public void SingleTap()
    {
        if (OnSingleTap != null)
        {
            OnSingleTap();
        }
    }

    // Returns "doubleTap" when double tap is detected, "singleTap" when single tap accordingly, 
    // "continious" when player is touching the screen and "none" when nothing happens.
    public string CheckInputSmartphone()
    {
        if (Input.touchCount > 0)
        {
            // Invoke screen touch event.
            onScreenTouch.Invoke();
            touch = Input.GetTouch(0);
            touchDuration += Time.deltaTime;

            // If player is touching a screen and makes another taps then counting the time of the second touch.
            if (Input.touchCount > 1)
            {
                touch = Input.GetTouch(1);
                if (touch.phase == TouchPhase.Began)
                    touchDuration = 0.0f;
                touchDuration += Time.deltaTime;
            }

            // Making sure it only check the touch once && it was a short touch/tap and not a dragging.
            if (touch.phase == TouchPhase.Ended && touchDuration < 0.3f) 
            {
                if (touch.tapCount > 1)
                {
                    return "doubleTap";
                }
                else
                {
                    SingleTap();
                    return "singleTap";
                }
            }
            else
            {
                return "continious";
            }

        }
        else
        {
            touchDuration = 0.0f;
            return "none";
        }
    }

}
