using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
    public int hpAmount = 20;

    [HideInInspector]
    public BasePlayerSettings playerSettingsScript;

    public void Awake()
    {
        playerSettingsScript = FindObjectOfType<BasePlayerSettings>();
    }

    public override void ActivateAbility()
    {
        playerSettingsScript.Heal(hpAmount);
    }
}