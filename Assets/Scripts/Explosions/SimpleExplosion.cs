using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleExplosion : KillingObjects, IPooledObject
{
    public float radius = 4f;

    private ObjectPooler pool;

    public void OnObjectSpawn()
    {
        pool = ObjectPooler.Instance;
        transform.localScale = new Vector3(radius, radius, radius);
    }

    public void OnObjectDestroy()
    {

    }

    void Start()
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }

    // Called when animation finished.
    public void SelfDestroy()
    {
        pool.ReturnToPool(gameObject);
    }
}
