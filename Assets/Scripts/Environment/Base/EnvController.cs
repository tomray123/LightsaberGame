using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : EnvManager
{
    List<Ability> OnHit = new List<Ability>();
    List<Ability> OnDestroy = new List<Ability>();
    List<Ability> OnAwake = new List<Ability>();
    List<Ability> OnEnable = new List<Ability>();

    protected override void Start()
    {
        base.Start();
        // Adding abilities to the according event lists.
        foreach (Ability a in envData.abilities)
        {
            switch (a.type)
            {
                case Ability.EventType.OnHit:
                    OnHit.Add(a);
                    break;

                case Ability.EventType.OnDestroy:
                    OnDestroy.Add(a);
                    break;

                case Ability.EventType.OnAwake:
                    OnAwake.Add(a);
                    break;

                case Ability.EventType.OnEnable:
                    OnEnable.Add(a);
                    break;
            }  
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (envData.hp > 0)
        {
            // Checking for bullet layer.
            if (collision.gameObject.layer == 8)
            {
                envData.hp -= collision.GetComponent<Bullet>().damage;

                // Add get hit visual.
                //
                //

                // Call the ability.
                foreach (Ability a in OnHit)
                {
                    a.ActivateAbility();
                }
            }

            // Checking for explosion layer.
            if (collision.gameObject.layer == 11)
            {
                envData.hp -= collision.GetComponent<SimpleExplosion>().damage;

                // Add get hit visual.
                //
                //

                // Call the ability.
                foreach (Ability a in OnHit)
                {
                    a.ActivateAbility();
                }
            }

            // Checking for lightsaber's hit.
            if (collision.CompareTag("LightSaber"))
            {
                envData.hp -= collision.GetComponent<SaberSettings>().damage;

                // Add get hit visual.
                //
                //

                // Call the ability.
                foreach (Ability a in OnHit)
                {
                    a.ActivateAbility();
                }
            }
        }
    }
}
