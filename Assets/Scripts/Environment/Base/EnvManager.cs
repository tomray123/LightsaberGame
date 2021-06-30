using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : ScorableObjects
{
    [HideInInspector]
    public EnvController envController;

    [HideInInspector]
    public EnvData envData;

    [HideInInspector]
    public EnvVisual envVisual;

    protected void Awake()
    {
        envController = GetComponent<EnvController>();
        envData = GetComponent<EnvData>();
        envVisual = GetComponent<EnvVisual>();
    }
}
