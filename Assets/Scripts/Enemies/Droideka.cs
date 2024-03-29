﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droideka : Enemy
{
    public Collider2D[] droidekaCol;
    public Transform barrelLeft;
    public Transform barrelRight;
    // Start is called before the first frame update
    void Start()
    {
        BaseInitialization();
        transform.GetChild(1).gameObject.GetComponent<Shield>().parameters[0].value = parameters[3].value;
        droidekaCol = gameObject.GetComponents<BoxCollider2D>();
        barrelLeft = transform.GetChild(2);
        barrelRight = transform.GetChild(3);
        for (int i = 0; i < droidekaCol.Length; i++)
        {
            droidekaCol[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoop) StartCoroutine(EnemyLoop(parameters[0].value, parameters[1].value, parameters[2].value));
    }

    private IEnumerator EnemyLoop(float initialTime, float shootTime, float flashesDuration)
    {
        startLoop = false;

        if (isJustBorn)
        {
            isJustBorn = false;
            yield return new WaitForSeconds(initialTime - flashesDuration);
        }
        for (int i = 0; i < 6; i++)
        {
            flash = !flash;
            shootIndicator.gameObject.SetActive(flash);
            yield return new WaitForSeconds(flashesDuration / 6);
        }
        Shoot();
        yield return new WaitForSeconds(shootTime - flashesDuration);

        startLoop = true;
    }

    protected override void Shoot()
    {
        GameObject bulletCloneLeft = Instantiate(bullet, barrelLeft.position, barrelLeft.rotation);
        bulletCloneLeft.GetComponent<Bullet>().damage += damage;
        GameObject bulletCloneRight = Instantiate(bullet, barrelRight.position, barrelRight.rotation);
        bulletCloneRight.GetComponent<Bullet>().damage += damage;
    }
}
