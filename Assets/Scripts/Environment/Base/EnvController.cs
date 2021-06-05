using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : EnvManager
{
    List<Ability> OnHitAbility = new List<Ability>();
    List<Ability> OnDestroyAbility = new List<Ability>();
    List<Ability> OnAwakeAbility = new List<Ability>();
    List<Ability> OnEnableAbility = new List<Ability>();

    bool isDestroyable = false;

    protected void Awake()
    {
        // Add visual.
        //
        //

        // Call the ability.
        foreach (Ability a in OnAwakeAbility)
        {
            a.ActivateAbility();
        }
    }

    protected override void Start()
    {
        base.Start();

        // Adding abilities to the according event lists.
        foreach (Ability a in envData.abilities)
        {
            switch (a.type)
            {
                case Ability.EventType.OnHit:
                    OnHitAbility.Add(a);
                    break;

                case Ability.EventType.OnDestroy:
                    OnDestroyAbility.Add(a);
                    break;

                case Ability.EventType.OnAwake:
                    OnAwakeAbility.Add(a);
                    break;

                case Ability.EventType.OnEnable:
                    OnEnableAbility.Add(a);
                    break;
            }
        }

        // Setting destroyability.
        if (envData.hp < 0)
        {
            isDestroyable = false;
        }
        else
        {
            isDestroyable = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyable)
        {
            // Checking for bullet layer.
            if (collision.gameObject.layer == 8)
            {
                envData.hp -= collision.GetComponent<Bullet>().damage;

                // Add get hit visual.
                //
                //

                // Call the ability.
                foreach (Ability a in OnHitAbility)
                {
                    a.ActivateAbility();
                }

                Destroy(collision);
            }

            // Checking for explosion layer.
            if (collision.gameObject.layer == 11)
            {
                envData.hp -= collision.GetComponent<SimpleExplosion>().damage;

                // Add get hit visual.
                //
                //

                // Call the ability.
                foreach (Ability a in OnHitAbility)
                {
                    a.ActivateAbility();
                }

                Destroy(collision);
            }

            // Checking for lightsaber's hit.
            if (collision.CompareTag("LightSaber"))
            {
                envData.hp -= collision.GetComponent<SaberSettings>().damage;

                // Add get hit visual.
                //
                //

                // Call the ability.
                foreach (Ability a in OnHitAbility)
                {
                    a.ActivateAbility();
                }
            }
            // Check for destroying.
            OnDeath();
        }
    }

    private void OnDestroy()
    {
        // Add get hit visual.
        //
        //

        // Call the ability.
        foreach (Ability a in OnDestroyAbility)
        {
            a.ActivateAbility();
        }
    }

    private void OnDeath()
    {
        if (envData.hp <= 0)
        {
            float time = 0;
            Destroy(gameObject, time);
        }
    }

    private void OnEnable()
    {
        // Add visual.
        //
        //

        // Call the ability.
        foreach (Ability a in OnEnableAbility)
        {
            a.ActivateAbility();
        }
    }
}
