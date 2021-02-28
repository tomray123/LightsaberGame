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
    public float spawnPeriod;
}


public class Spawner : MonoBehaviour
{

    [SerializeField]
    public List<SpawnObject> MyList = new List<SpawnObject>();

    public static int TotalNumberOfEnemies = -1;

    void Start()
    {
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
        Instantiate(spawnObj.obj, spawnObj.spawnPos.position, Quaternion.identity);
    }
}
