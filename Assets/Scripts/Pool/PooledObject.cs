using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject: MonoBehaviour
{
    [HideInInspector]
    public string poolTag;
    [HideInInspector]
    public int objIdPool;
}
