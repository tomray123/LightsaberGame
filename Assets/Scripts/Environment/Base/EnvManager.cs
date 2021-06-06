using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    [HideInInspector]
    public EnvController envController;

    [HideInInspector]
    public EnvData envData;

    [HideInInspector]
    public EnvVisual envVisual;

    void Awake()
    {
        envController = GetComponent<EnvController>();
        envData = GetComponent<EnvData>();
        envVisual = GetComponent<EnvVisual>();
    }
}
