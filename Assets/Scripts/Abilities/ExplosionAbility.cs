using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAbility : Ability
{
    public int damage = 100;

    public GameObject explosionPrefab;

    public override void ActivateAbility()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
