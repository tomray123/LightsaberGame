using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaberSettings : KillingObjects
{
    [SerializeField] private VoidEventChannelSO reflectionEventChannel = default;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Raising reflection event if lightsaber touched bullet.
        if (collision.gameObject.layer == 8)
        {
            reflectionEventChannel.RaiseEvent();
        }
    }
}
