using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAbility : Ability
{
    public GameObject explosionPrefab;

    [HideInInspector]
    public ObjectPooler pool;

    private void Start()
    {
        pool = ObjectPooler.Instance;
    }

    public override void ActivateAbility()
    {
        pool.SpawnFromPool(explosionPrefab, transform.position, Quaternion.identity);
    }
}
