﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        BaseInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoop) StartCoroutine(EnemyLoop(parameters[0].value, parameters[1].value, parameters[2].value, parameters[3].value, parameters[4].value, parameters[5].value, parameters[6].value));
    }

    private IEnumerator EnemyLoop(float initialTime, float singleShootTime, float timeBeforeMulti, float gunSpinTime, float multiShootTime, float recharge, float flashesDuration)
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
        yield return new WaitForSeconds(singleShootTime - flashesDuration);
        for (int i = 0; i < 6; i++)
        {
            flash = !flash;
            shootIndicator.gameObject.SetActive(flash);
            yield return new WaitForSeconds(flashesDuration / 6);
        }
        Shoot();
        yield return new WaitForSeconds(timeBeforeMulti);

        StartCoroutine(SetTransparency(gunSpinTime, 0f, 0.7f, transform.GetChild(1).gameObject));
        yield return new WaitForSeconds(gunSpinTime);

        Shoot();
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(multiShootTime / 5);
            Shoot();
        }

        StartCoroutine(SetTransparency(gunSpinTime, 0.7f, 0f, transform.GetChild(1).gameObject));
        yield return new WaitForSeconds(recharge);

        startLoop = true;
    }

    protected override IEnumerator GetHit()
    {
        SpriteRenderer renderer = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        Color cl = new Color(1f, 1f, 1f, 0f);
        for (float i = 0f; i <= 0.7f; i += 0.035f)
        {
            cl = new Color(1f, 1f, 1f, i);
            renderer.color = cl;
            yield return null;
        }
        for (float i = 0.7f; i >= 0f; i -= 0.035f)
        {
            cl = new Color(1f, 1f, 1f, i);
            renderer.color = cl;
            yield return null;
        }
        cl = new Color(1f, 1f, 1f, 0f);
        renderer.color = cl;
        DestroyWhenDead();
    }
}
