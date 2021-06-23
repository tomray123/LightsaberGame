using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScorableObjects : MonoBehaviour
{
    public int scorePrice = 0;

    public Action<int, int> OnDeath;

    // Subscribing on death action.
    protected virtual void Start()
    {
        OnDeath += ScoreCounter.instance.CountActionScore;
    }

    public void ObjectDeath(int factor)
    {
        if (OnDeath != null)
        {
            OnDeath(scorePrice, factor);
        }
        OnDeath -= ScoreCounter.instance.CountActionScore;
    }
}
