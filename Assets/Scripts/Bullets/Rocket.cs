using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocket : MonoBehaviour
{
    public Vector3 targetLocation = Vector3.zero;

    public float moveDuration = 1.0f;

    public Ease moveEase = Ease.Linear;

    public float damageRadius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchRocket()
    {
        transform.DOMove(targetLocation, moveDuration).SetEase(moveEase);
    }
}
