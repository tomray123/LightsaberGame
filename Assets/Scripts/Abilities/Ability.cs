using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public enum EventType
    {
        OnHit,
        OnDestroy,
        OnAwake,
        OnEnable
    }

    public EventType type = EventType.OnHit;

    public virtual void ActivateAbility()
    {

    }
}
