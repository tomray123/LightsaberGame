using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class VE
{
    public string tag;
    public GameObject visualEffect;
}

public class VisualEffects : MonoBehaviour
{
    public List<VE> visualEffects;

    private Dictionary<string, GameObject> veDict;

    private ObjectPooler pool;

    // Start is called before the first frame update
    void Awake()
    {
        veDict = new Dictionary<string, GameObject>();

        foreach(VE ve in visualEffects)
        {
            veDict.Add(ve.tag, ve.visualEffect);
        }
    }

    private void Start()
    {
        pool = ObjectPooler.Instance;
    }

    public void ActivateVisualEffect(string tag, Vector3 position, Quaternion rot)
    {
        if (!veDict.ContainsKey(tag))
        {
            Debug.LogError(gameObject + " doesn't contain visual effect with key " + tag + "!");
            return;
        }
        pool.SpawnFromPool(veDict[tag], position, rot);
    }

    public void ActivateVisualEffect(int index, Vector3 position, Quaternion rot)
    {
        if (index >= visualEffects.Count || index < 0)
        {
            Debug.LogError(gameObject + " doesn't contain visual effect with index " + index + "!");
            return;
        }
        pool.SpawnFromPool(visualEffects[index].visualEffect, position, rot);
    }
}
