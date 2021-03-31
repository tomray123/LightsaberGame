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
    [Header("Parameter 1")]
    public string p1Name;
    public float p1Value;
    [Header("Parameter 2")]
    public string p2Name;
    public float p2Value;
    [Header("Parameter 3")]
    public string p3Name;
    public float p3Value;
    [Header("Parameter 4")]
    public string p4Name;
    public float p4Value;
    [Header("Parameter 5")]
    public string p5Name;
    public float p5Value;
    [Header("Parameter 6")]
    public string p6Name;
    public float p6Value;

    public SpawnObject(GameObject _obj = null, Transform _spawnPos = null, float _spawnPeriod = 0, string _p1Name = "none",
        float _p1Value = 0f, string _p2Name = "none",
        float _p2Value = 0f, string _p3Name = "none",
        float _p3Value = 0f, string _p4Name = "none",
        float _p4Value = 0f, string _p5Name = "none",
        float _p5Value = 0f, string _p6Name = "none",
        float _p6Value = 0f)
    {
        obj = _obj;
        spawnPos = _spawnPos;
        spawnPeriod = _spawnPeriod;
        p1Name = _p1Name;
        p1Value = _p1Value;
        p2Name = _p2Name;
        p2Value = _p2Value;
        p3Name = _p3Name;
        p3Value = _p3Value;
        p4Name = _p4Name;
        p4Value = _p4Value;
        p5Name = _p5Name;
        p5Value = _p5Value;
        p6Name = _p6Name;
        p6Value = _p6Value;
    }
}


public class Spawner : MonoBehaviour
{

    [SerializeField]
    [Header("Set spawn period of the first object to all")]
    public bool allSpawnPeriod = false;

    /*
    [SerializeField]
    [Header("Set time before the first shot of the first object to all")]
    public bool allTimeToInitialShoot = false;

    [SerializeField]
    [Header("Set time interval for shooting of the first object to all")]
    public bool allShootPeriod = false;
    */

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
        /*if (allTimeToInitialShoot)
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
        }*/
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

        
        Enemy enemy = SpawnedObject.GetComponent<Enemy>();
        
        if (enemy.parameters.Count > 5)
        {
            enemy.parameters[0].value = spawnObj.p1Value;
            enemy.parameters[1].value = spawnObj.p2Value;
            enemy.parameters[2].value = spawnObj.p3Value;
            enemy.parameters[3].value = spawnObj.p4Value;
            enemy.parameters[4].value = spawnObj.p5Value;
            enemy.parameters[5].value = spawnObj.p6Value;
        }
        
        
        
        

        //SpawnedObject.GetComponent<Enemy>().timeToShoot = spawnObj.shootPeriod;
    }
}
