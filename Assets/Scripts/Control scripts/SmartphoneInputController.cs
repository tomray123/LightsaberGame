using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartphoneInputController : MonoBehaviour
{
    public Touch touch;

    public float touchDuration;

    public static SmartphoneInputController instance;

    public Action OnSingleTap;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SingleTap()
    {
        if (OnSingleTap != null)
        {
            OnSingleTap();
        }
    }

    public string CheckInputSmartphone()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchDuration += Time.deltaTime;

            if (Input.touchCount > 1)
            {
                touch = Input.GetTouch(1);
                if (touch.phase == TouchPhase.Began)
                    touchDuration = 0.0f;
                touchDuration += Time.deltaTime;
            }

            if (touch.phase == TouchPhase.Ended && touchDuration < 0.3f) //making sure it only check the touch once && it was a short touch/tap and not a dragging.
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
