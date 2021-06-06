using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public enum EventType
    {
        OnHit,
        OnDestroy,
        OnCreate,
        OnEnable
    }

    // Some additional information like ability type.
    public string extraData = "";

    public EventType type = EventType.OnHit;

    public virtual void ActivateAbility()
    {

    }
}
