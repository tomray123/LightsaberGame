using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

// Custom Unity editor menu was created for this script.
// You can find it in Editor folder -> SpawnerEditor.

// Class of objects to spawn.
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
    [Header("Parameter 7")]
    public string p7Name;
    public float p7Value;
    [Header("Parameter 8")]
    public string p8Name;
    public float p8Value;

    // Constructor for spawn objects.
    public SpawnObject(GameObject _obj = null, Transform _spawnPos = null, float _spawnPeriod = 0, string _p1Name = "none",
        float _p1Value = 0f, string _p2Name = "none",
        float _p2Value = 0f, string _p3Name = "none",
        float _p3Value = 0f, string _p4Name = "none",
        float _p4Value = 0f, string _p5Name = "none",
        float _p5Value = 0f, string _p6Name = "none",
        float _p6Value = 0f, string _p7Name = "none",
        float _p7Value = 0f, string _p8Name = "none",
        float _p8Value = 0f)
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
        p7Name = _p7Name;
        p7Value = _p7Value;
        p8Name = _p8Name;
        p8Value = _p8Value;
    }
}

public class Spawner : MonoBehaviour
{
    // This setting allows to set the same spawn period for every spawn object.
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

    // List of objects to spawn.
    [SerializeField]
    public List<SpawnObject> MyList = new List<SpawnObject>();

    public Dictionary<Transform, Queue<SpawnObject>> spawnPointDictionary = new Dictionary<Transform, Queue<SpawnObject>>();

    // Amount of all enemies spawned on one level.
    public static int TotalNumberOfEnemies = -1;

    void Start()
    {
        // Adding spawn objects to the queue.
        for (int i = 0; i < MyList.Count; i++)
        {
            if (spawnPointDictionary.ContainsKey(MyList[i].spawnPos))
            {
                spawnPointDictionary[MyList[i].spawnPos].Enqueue(MyList[i]);
            }
            else
            {
                spawnPointDictionary[MyList[i].spawnPos] = new Queue<SpawnObject>();
            }
        }

        if (allSpawnPeriod)
        {
            // Setting the same spawn period for every spawn object.
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

        /*
        // Starting a spawn timer for every spawn object.
        for (int i = 0; i < MyList.Count; i++)
        {
            StartCoroutine(Timer(MyList[i], i));
        }
        */

        StartCoroutine(StartInitialTimer(0));

        // Setting an amount of all enemies spawned on one level.
        TotalNumberOfEnemies = MyList.Count;
    }

    // Starts a timer to spawn and sets all spawn object parameters.
    private IEnumerator StartInitialTimer(int num)
    {
        /*
        // Set the timer for first spawn object in list.
        if (num == 0)
        {
            yield return new WaitForSeconds(spawnObj.spawnPeriod);
        }
        // Set the timer for other spawn objects in list.
        else
        {
            yield return new WaitForSeconds(spawnObj.spawnPeriod * num);
        }
        */

        yield return new WaitForSeconds(MyList[num].spawnPeriod);

        // Creating an object and getting its enemy script.
        GameObject SpawnedObject = Instantiate(MyList[num].obj, MyList[num].spawnPos.position, Quaternion.identity);
        Enemy enemy = SpawnedObject.GetComponent<Enemy>();

        // Setting all parameters for objects.
        if (enemy.parameters.Count > 7)
        {
            enemy.parameters[0].value = MyList[num].p1Value;
            enemy.parameters[1].value = MyList[num].p2Value;
            enemy.parameters[2].value = MyList[num].p3Value;
            enemy.parameters[3].value = MyList[num].p4Value;
            enemy.parameters[4].value = MyList[num].p5Value;
            enemy.parameters[5].value = MyList[num].p6Value;
            enemy.parameters[6].value = MyList[num].p7Value;
            enemy.parameters[7].value = MyList[num].p8Value;
        }

        num++;

        if (num < MyList.Count)
        {  
            StartCoroutine(StartInitialTimer(num));
        }
    }
}
