using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScorableObjects : MonoBehaviour, IPooledObject
{
    public int scorePrice = 0;

    public Action<int, int> OnDeath;

    protected ObjectPooler pool;

    // Subscribing on death action.
    protected virtual void Start()
    {
        pool = ObjectPooler.Instance;
    }

    public virtual void OnObjectSpawn()
    {
        OnDeath += ScoreCounter.instance.CountActionScore;
    }

    public virtual void OnObjectDestroy()
    {
        OnDeath -= ScoreCounter.instance.CountActionScore;
    }

    public void ObjectDeath(int factor)
    {
        if (OnDeath != null)
        {
            OnDeath(scorePrice, factor);
        }
    }
}
