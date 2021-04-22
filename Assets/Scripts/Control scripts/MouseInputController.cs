using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputController : MonoBehaviour
{
    public static MouseInputController instance;

    public Action OnSingleClick;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SingleClick()
    {
        if (OnSingleClick != null)
        {
            OnSingleClick();
        }
    }
}
