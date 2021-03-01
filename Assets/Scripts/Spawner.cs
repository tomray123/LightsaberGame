using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class SpawnObject
{
    [Header("Object to spawn")]
    public GameObject obj;
    [Header("Spawn Position")]
    public Transform spawnPos;
    [Header("Time interval for spawning")]
    public float spawnPeriod;
    [Header("Time before the first shot")]
    public float timeToInitialShoot = 4.3f;
    [Header("Time interval for shooting")]
    public float shootPeriod = 0.8f;
}


public class Spawner : MonoBehaviour
{

    [SerializeField]
    [Header("Set spawn period of the first object to all")]
    public bool allSpawnPeriod = false;

    [SerializeField]
    [Header("Set time before the first shot of the first object to all")]
    public bool allTimeToInitialShoot = false;

    [SerializeField]
    [Header("Set time interval for shooting of the first object to all")]
    public bool allShootPeriod = false;

    [Space(10)]

    [SerializeField]
    public List<SpawnObject> MyList = new List<SpawnObject>();

    public static int TotalNumberOfEnemies = -1;

    void Start()
    {
        if (allSpawnPeriod)
        {
            for (int i = 0; i < MyList.Count; i++)       
            {
                MyList[i].spawnPeriod = MyList[0].spawnPeriod;
            }
        }
        if (allTimeToInitialShoot)
        {
            for (int i = 0; i < MyList.Count; i++)      
            {
                MyList[i].timeToInitialShoot = MyList[0].timeToInitialShoot;
            }
        }
        if (allShootPeriod)
        {
            for (int i = 0; i < MyList.Count; i++)     
            {
                MyList[i].shootPeriod = MyList[0].shootPeriod;
            }
        }
        for (int i = 0; i < MyList.Count; i++)       //(SpawnObject obj in MyList)
        {
            StartCoroutine(Timer(MyList[i], i));
        }
        TotalNumberOfEnemies = MyList.Count;
    }

    void Update()
    {
        
    }

    private IEnumerator Timer(SpawnObject spawnObj, int num)
    {
        if (num == 0)
        {
            yield return new WaitForSeconds(spawnObj.spawnPeriod);
        }
        else
        {
            yield return new WaitForSeconds(spawnObj.spawnPeriod * num);
        }
        GameObject SpawnedObject = Instantiate(spawnObj.obj, spawnObj.spawnPos.position, Quaternion.identity);
        SpawnedObject.GetComponent<Enemy>().timeToFirstShoot = spawnObj.timeToInitialShoot;
        SpawnedObject.GetComponent<Enemy>().timeToShoot = spawnObj.shootPeriod;
    }
}
