using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public float timeToShoot = 1f;
    public GameObject bullet;
    bool isTimeToShoot = true;

    void Start()
    {
        transform.up = target.transform.position - transform.position;
    }

    void Update()
    {
        if(isTimeToShoot) StartCoroutine(Shoot(timeToShoot));
    }

    private IEnumerator Shoot(float time)
    {
        Instantiate(bullet, transform.position, transform.rotation);
        isTimeToShoot = false;
        yield return new WaitForSeconds(time);
        isTimeToShoot = true;
    }
}
