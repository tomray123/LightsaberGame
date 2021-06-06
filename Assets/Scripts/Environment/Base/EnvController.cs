using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : EnvManager
{
    bool isDestroyable = false;

    protected void Start()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking for bullet layer.
        if (collision.gameObject.layer == 8)
        {
            if (isDestroyable)
            {
                envData.hp -= (collision.GetComponent<Bullet>().damage - envData.modificator) * envData.modificator + 1;
            }

            // Add get hit visual.
            //
            //

            // Call the ability.
            foreach (Ability a in envData.OnHitAbility)
            {
                a.ActivateAbility();
            }

            Destroy(collision);
        }

        // Checking for explosion layer.
        if (collision.gameObject.layer == 11)
        {
            if (isDestroyable)
            {
                envData.hp -= collision.GetComponent<SimpleExplosion>().damage * envData.modificator;
            }

            // Add get hit visual.
            //
            //

            // Call the ability.
            foreach (Ability a in envData.OnHitAbility)
            {
                a.ActivateAbility();
            }

            Destroy(collision);
        }

        // Checking for lightsaber's hit.
        if (collision.CompareTag("LightSaber"))
        {
            if (isDestroyable)
            {
                envData.hp -= (collision.GetComponent<SaberSettings>().damage - envData.modificator) * envData.modificator + 1;
            }

            // Add get hit visual.
            //
            //

            // Call the ability.
            foreach (Ability a in envData.OnHitAbility)
            {
                a.ActivateAbility();
            }
        }
        // Check for destroying.
        OnDeath();
    }

    private void OnDeath()
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

            Destroy(gameObject);
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
