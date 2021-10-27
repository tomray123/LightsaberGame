﻿using System.Collections;
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

// Class for storing spawn position and queue of spawn objects to this position.
public class SpawnPoint
{
    public Transform spawnPosition;
    public Queue<SpawnObject> objectQueue;

    public SpawnPoint(Transform pos, SpawnObject obj)
    {
        spawnPosition = pos;
        objectQueue = new Queue<SpawnObject>();
        objectQueue.Enqueue(obj);
    }
}

public class Spawner : MonoBehaviour
{
    // Singleton instance.
    public static Spawner instance;

    private ObjectPooler pool;

    // This setting allows to set the same spawn period for every spawn object.
    [SerializeField]
    [Header("Set spawn period of the first object to all")]
    public bool allSpawnPeriod = false;

    [Space]

    [Header("Use timer (default) or spawn manually.")]
    public bool useTimer = true;

    [Space(10)]

    // List of objects to spawn.
    [SerializeField]
    public List<SpawnObject> MyList = new List<SpawnObject>();

    public List<SpawnPoint> spawnPointList = new List<SpawnPoint>();

    // Amount of all enemies spawned on one level.
    public static int TotalNumberOfEnemies = -1;

    public static int NumberOfKilledEnemies = -1;

    public Action onAllEnemiesDead;

    // Used in SpawnNext function to count enemies.
    private int currentIndex = 0;

    private void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        pool = ObjectPooler.Instance;

        // Adding spawn objects to the queue.
        for (int i = 0; i < MyList.Count; i++)
        {
            // Searching for position.
            SpawnPoint point = spawnPointList.Find(x => x.spawnPosition == MyList[i].spawnPos);
            if (point != null)
            {
                spawnPointList[spawnPointList.IndexOf(point)].objectQueue.Enqueue(MyList[i]);
            }
            else
            {
                spawnPointList.Add(new SpawnPoint(MyList[i].spawnPos, MyList[i]));
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

        if (useTimer)
        {
            StartCoroutine(StartInitialTimer(0));
        }

        // Setting an amount of all enemies spawned on one level.
        TotalNumberOfEnemies = MyList.Count;

        NumberOfKilledEnemies = 0;
    }

    public void OnAllEnemiesKilled()
    {
        if (onAllEnemiesDead != null)
        {
            onAllEnemiesDead();
        }
    }

    // Starts coroutine to spawn another enemy on the position of previous enemy (if it's dead).
    public void SpawnAnotherEnemy(Enemy enemy, int index)
    {
        NumberOfKilledEnemies++;
        if (NumberOfKilledEnemies == TotalNumberOfEnemies)
        {
            NumberOfKilledEnemies = 0;
            OnAllEnemiesKilled();
        }

        if (spawnPointList[index].objectQueue.Count > 0)
        {
            StartCoroutine(SpawnFromQueue(index));
        }
        // Unsubscribing from enemy OnDeath event. 
        enemy.OnSpawnObjectDeath -= SpawnAnotherEnemy;
    }

    // Starts a timer to spawn and sets all spawn object parameters.
    private IEnumerator StartInitialTimer(int num)
    {
        if (spawnPointList[num].objectQueue.Count <= 0)
        {
            Debug.LogWarning("No objects to spawn");
            yield break;
        }

        // Getting object from queue.
        SpawnObject spawnObj = spawnPointList[num].objectQueue.Dequeue();

        // Checking for negative spawnPeriod.
        if (spawnObj.spawnPeriod < 0)
        {
            spawnObj.spawnPeriod = 0;
        }

        yield return new WaitForSeconds(spawnObj.spawnPeriod);

        // Creating an object and getting its enemy script.
        GameObject SpawnedObject = pool.SpawnFromPool(spawnObj.obj, spawnPointList[num].spawnPosition.position, Quaternion.identity);

        Enemy enemy = SpawnedObject.GetComponent<Enemy>();
        // Subscribing to enemy OnDeath event. 
        enemy.OnSpawnObjectDeath += SpawnAnotherEnemy;

        // Setting all parameters for objects.
        enemy.spawnIndex = num;
        if (enemy.parameters.Count > 7)
        {
            enemy.parameters[0].value = spawnObj.p1Value;
            enemy.parameters[1].value = spawnObj.p2Value;
            enemy.parameters[2].value = spawnObj.p3Value;
            enemy.parameters[3].value = spawnObj.p4Value;
            enemy.parameters[4].value = spawnObj.p5Value;
            enemy.parameters[5].value = spawnObj.p6Value;
            enemy.parameters[6].value = spawnObj.p7Value;
            enemy.parameters[7].value = spawnObj.p8Value;
        }

        num++;

        if (num < spawnPointList.Count)
        {
            StartCoroutine(StartInitialTimer(num));
        }
    }

    // Spawns objects from queue of spawn points.
    private IEnumerator SpawnFromQueue(int num)
    {
        // Getting object from queue.
        SpawnObject spawnObj = spawnPointList[num].objectQueue.Dequeue();

        // Checking for negative spawnPeriod.
        if (spawnObj.spawnPeriod < 0)
        {
            spawnObj.spawnPeriod = 0;
        }

        yield return new WaitForSeconds(spawnObj.spawnPeriod);

        // Creating an object and getting its enemy script.
        GameObject SpawnedObject = pool.SpawnFromPool(spawnObj.obj, spawnPointList[num].spawnPosition.position, Quaternion.identity);

        Enemy enemy = SpawnedObject.GetComponent<Enemy>();
        // Subscribing to enemy OnDeath event. 
        enemy.OnSpawnObjectDeath += SpawnAnotherEnemy;

        // Setting all parameters for objects.
        enemy.spawnIndex = num;
        if (enemy.parameters.Count > 7)
        {
            enemy.parameters[0].value = spawnObj.p1Value;
            enemy.parameters[1].value = spawnObj.p2Value;
            enemy.parameters[2].value = spawnObj.p3Value;
            enemy.parameters[3].value = spawnObj.p4Value;
            enemy.parameters[4].value = spawnObj.p5Value;
            enemy.parameters[5].value = spawnObj.p6Value;
            enemy.parameters[6].value = spawnObj.p7Value;
            enemy.parameters[7].value = spawnObj.p8Value;
        }
    }

    // Function for spawning enemies without spawn timer.
    public GameObject SpawnNext()
    {
        if (currentIndex < MyList.Count)
        {
            // Searching for right spawn point.
            SpawnPoint point = spawnPointList.Find(x => x.spawnPosition == MyList[currentIndex].spawnPos);

            if (point.objectQueue.Count <= 0)
            {
                Debug.LogWarning("No objects to spawn");
                return null;
            }

            // Getting object from queue.
            SpawnObject spawnObj = point.objectQueue.Dequeue();

            // Creating an object and getting its enemy script.
            GameObject SpawnedObject = pool.SpawnFromPool(spawnObj.obj, point.spawnPosition.position, Quaternion.identity);

            Enemy enemy = SpawnedObject.GetComponent<Enemy>();

            // Setting all parameters for objects.
            enemy.spawnIndex = currentIndex;

            if (enemy.parameters.Count > 7)
            {
                enemy.parameters[0].value = spawnObj.p1Value;
                enemy.parameters[1].value = spawnObj.p2Value;
                enemy.parameters[2].value = spawnObj.p3Value;
                enemy.parameters[3].value = spawnObj.p4Value;
                enemy.parameters[4].value = spawnObj.p5Value;
                enemy.parameters[5].value = spawnObj.p6Value;
                enemy.parameters[6].value = spawnObj.p7Value;
                enemy.parameters[7].value = spawnObj.p8Value;
            }

            currentIndex++;

            return SpawnedObject;
        }

        return null;
    }
}
