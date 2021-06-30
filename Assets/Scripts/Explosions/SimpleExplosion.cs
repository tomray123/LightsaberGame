using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleExplosion : KillingObjects
{
    public float radius = 4f;

    void Start()
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }

    // Called when animation finished.
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
