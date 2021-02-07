﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public float timeToShoot = 1f;
    public GameObject bullet;
    private bool isTimeToShoot = true;

    private bool isJustBorn = true;

    public bool isLast = false;

    public static bool isWin = false;

    private Renderer renderer;

    private Transform shootIndicator;

    private bool flash = false;

    void Start()
    {
        transform.up = target.transform.position - transform.position;

        isJustBorn = true;

        renderer = GetComponent<Renderer>();

        shootIndicator = gameObject.transform.GetChild(0);
    }

    void Update()
    {
        if(isTimeToShoot) StartCoroutine(Shoot(timeToShoot));
    }

    private IEnumerator Shoot(float time)
    {
        isTimeToShoot = false;
        if (isJustBorn)
        {
            isJustBorn = false;
            yield return new WaitForSeconds(0.8f);
            
            for (int i=0; i<6; i++)
            {
                flash = !flash;
                shootIndicator.gameObject.SetActive(flash);
                yield return new WaitForSeconds(0.2f);
            }

        }
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(time - 1.2f);
        for (int i = 0; i < 6; i++)
        {
            flash = !flash;
            shootIndicator.gameObject.SetActive(flash);
            yield return new WaitForSeconds(0.2f);
        }
        isTimeToShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for bullet layer and for danger
        if(other.gameObject.layer == 8 && other.gameObject.GetComponent<Bullet>().isDangerous)
        {
            Destroy(other.gameObject);
            StartCoroutine(ChangeColor());
            //Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Bullet>().isDangerous = true;
        }
    }

    IEnumerator ChangeColor()
    {
        for (float i = 1f; i >= 0; i-=0.05f)
        {
            Color cl = new Color(1, i, i);
            renderer.material.color = cl;
            yield return null;
        }
        for (float i = 0; i < 1f; i += 0.05f)
        {
            Color cl = new Color(1, i, i);
            renderer.material.color = cl;
            yield return null;
        }
        Destroy(gameObject);

        if (isLast)
        {
            isWin = true;
        }
    }
}
