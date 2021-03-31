using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    void Start()
    {
        BaseInitialization();
    }


    void Update()
    {
        if (isTimeToShoot) StartCoroutine(EnemyLoop(parameters[0].value, parameters[1].value, parameters[2].value));
    }

    private IEnumerator EnemyLoop(float initialTime, float shootTime, float flashesDuration)
    {
        isTimeToShoot = false;
        if (isJustBorn)
        {
            isJustBorn = false;
            yield return new WaitForSeconds(initialTime - flashesDuration);

            for (int i = 0; i < 6; i++)
            {
                flash = !flash;
                shootIndicator.gameObject.SetActive(flash);
                yield return new WaitForSeconds(flashesDuration / 6);
            }
        }
        Shoot();
        yield return new WaitForSeconds(shootTime - flashesDuration);
        for (int i = 0; i < 6; i++)
        {
            flash = !flash;
            shootIndicator.gameObject.SetActive(flash);
            yield return new WaitForSeconds(0.2f);
        }
        isTimeToShoot = true;
    }
}
