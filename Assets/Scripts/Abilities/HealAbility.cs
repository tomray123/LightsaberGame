using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
    public int hpAmount = 20;
    public int numberOfHits = 1;

    [HideInInspector]
    public BasePlayerSettings playerSettingsScript;

    public void Awake()
    {
        playerSettingsScript = FindObjectOfType<BasePlayerSettings>();
    }

    public override void ActivateAbility()
    {
        playerSettingsScript.Heal(hpAmount);
        numberOfHits--;
        if (numberOfHits <= 0)
        {
            Destroy(gameObject);
        }
    }
}