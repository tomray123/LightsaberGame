using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : EnvManager, IPooledObject
{
    bool isDestroyable = false;

    protected override void Start()
    {
        base.Start();

        // Setting destroyability.
        if (envData.hp < 0)
        {
            isDestroyable = false;
        }
        else
        {
            isDestroyable = true;
        }

        // Add visual.
        //
        //

        // Call the ability.
        foreach (Ability a in envData.OnCreateAbility)
        {
            a.ActivateAbility();
        }
    }

    public override void OnObjectSpawn()
    {
        base.Start();
        base.OnObjectSpawn();

        // Setting destroyability.
        if (envData.hp < 0)
        {
            isDestroyable = false;
        }
        else
        {
            isDestroyable = true;
        }

        // Add visual.
        //
        //

        // Call the ability.
        foreach (Ability a in envData.OnCreateAbility)
        {
            a.ActivateAbility();
        }
    }

    public override void OnObjectDestroy()
    {
        base.OnObjectDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KillingObjects killer = collision.GetComponent<KillingObjects>();

        if (killer != null)
        {
            if (isDestroyable)
            {
                envData.hp -= (killer.damage - envData.modificator) * envData.modificator + 1;
            }

            // Generating death action to score system.
            if (envData.hp <= 0)
            {
                ObjectDeath(killer.factor);
            }

            // Add get hit visual.
            //
            //

            // Call the ability.
            foreach (Ability a in envData.OnHitAbility)
            {
                a.ActivateAbility();
            }

            // Checking for bullet layer.
            if (collision.gameObject.layer == 8)
            {
                pool.ReturnToPool(collision.gameObject);
            }
        }

        // Check for destroying.
        OnEnvDeath();
    }

    private void OnEnvDeath()
    {
        if (envData.hp <= 0)
        {
            // Add get hit visual.
            //
            //

            // Call the ability.
            foreach (Ability a in envData.OnDestroyAbility)
            {
                a.ActivateAbility();
            }

            pool.ReturnToPool(gameObject);
        }
    }

    private void OnEnable()
    {
        // Add visual.
        //
        //

        // Call the ability.
        if (envData.OnEnableAbility.Count > 0)
        {
            foreach (Ability a in envData.OnEnableAbility)
            {
                a.ActivateAbility();
            }
        }
    }
}
