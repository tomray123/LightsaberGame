using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleExplosion : MonoBehaviour
{
    public int damage = 100;

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
