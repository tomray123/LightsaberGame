using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvData : EnvManager
{
    public int hp = -10;

    [HideInInspector]
    public Ability[] abilities;

    protected override void Start()
    {
        base.Start();
        abilities = GetComponents<Ability>();
    }
}
