﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseInputController : MonoBehaviour
{
    // Singleton instance.
    public static MouseInputController instance;

    public Action OnSingleClick;

    public UnityEvent onScreenClick;

    public void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }
    }

    // Calls the OnSingleClick action when single click.
    public void SingleClick()
    {
        if (OnSingleClick != null)
        {
            OnSingleClick();
        }
    }
}
