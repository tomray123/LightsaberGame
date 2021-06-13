using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvData : EnvManager
{
    public int hp = -10;

    [HideInInspector]
    public Ability[] abilities;

    [HideInInspector]
    // Intended to set the damage to the object.
    public int modificator = 1;

    [HideInInspector]
    public List<Ability> OnHitAbility = new List<Ability>();
    [HideInInspector]
    public List<Ability> OnDestroyAbility = new List<Ability>();
    [HideInInspector]
    public List<Ability> OnCreateAbility = new List<Ability>();
    [HideInInspector]
    public List<Ability> OnEnableAbility = new List<Ability>();

    protected override void Start()
    {
        base.Start();

        // Normal damage.
        modificator = 1;

        abilities = GetComponents<Ability>();

        // Adding abilities to the according event lists.
        foreach (Ability a in abilities)
        {
            // Damage for healing box.
            if (a.extraData == "Heal")
            {
                modificator = 0;
            }

            switch (a.type)
            {
                case Ability.EventType.OnHit:
                    OnHitAbility.Add(a);
                    break;

                case Ability.EventType.OnDestroy:
                    OnDestroyAbility.Add(a);
                    break;

                case Ability.EventType.OnCreate:
                    OnCreateAbility.Add(a);
                    break;

                case Ability.EventType.OnEnable:
                    OnEnableAbility.Add(a);
                    break;
            }
        }
    }
}
