using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droideka : Enemy
{
    public Collider2D droidekaCol;
    public Transform barrelLeft;
    public Transform barrelRight;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        BaseInitialization();
        transform.GetChild(1).gameObject.GetComponent<Shield>().parameters[0].value = parameters[3].value;
        droidekaCol = gameObject.GetComponent<BoxCollider2D>();
        barrelLeft = transform.GetChild(2);
        barrelRight = transform.GetChild(3);
        droidekaCol.enabled = false;
        /*
        for (int i = 0; i < droidekaCol.Length; i++)
        {
            droidekaCol[i].enabled = false;
        }
        */
    }

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        BaseInitialization();
        transform.GetChild(1).gameObject.GetComponent<Shield>().parameters[0].value = parameters[3].value;
        droidekaCol = gameObject.GetComponent<BoxCollider2D>();
        barrelLeft = transform.GetChild(2);
        barrelRight = transform.GetChild(3);
        droidekaCol.enabled = false;
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
        Bullet bulletCloneLeft = pool.SpawnFromPool(bullet, barrelLeft.position, barrelLeft.rotation).GetComponent<Bullet>();
        bulletCloneLeft.damage += damage;
        bulletCloneLeft.shooter = gameObject;
        Bullet bulletCloneRight = pool.SpawnFromPool(bullet, barrelRight.position, barrelRight.rotation).GetComponent<Bullet>();
        bulletCloneRight.damage += damage;
        bulletCloneRight.shooter = gameObject;
    }
}
