using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class SpawnObject
{
    public GameObject obj;
    public Transform spawnPos;
    public float spawnTime;
}


public class Spawner : MonoBehaviour
{
    [SerializeField]
    public List<SpawnObject> MyList = new List<SpawnObject>();
    void Start()
    {
        foreach(SpawnObject obj in MyList)
        {
            StartCoroutine(Timer(obj));
        }
    }

    void Update()
    {
        
    }

    private IEnumerator Timer(SpawnObject spawnObj)
    {
        yield return new WaitForSeconds(spawnObj.spawnTime);
        Instantiate(spawnObj.obj, spawnObj.spawnPos.position, Quaternion.identity);
    }
}
