using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy
{
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        BaseInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoop) StartCoroutine(EnemyLoop(parameters[0].value, parameters[1].value, parameters[2].value, parameters[3].value));
    }

    private IEnumerator EnemyLoop(float initialTime, float aimingTime, float cooldownTime, float freezeTime)
    {
        startLoop = false;
        if (isJustBorn)
        {
            isJustBorn = false;
            yield return new WaitForSeconds(initialTime);
        }
        line.enabled = true;
        StartCoroutine(SetTransparency(aimingTime, 0f, 1f, transform.GetChild(0).gameObject));
        yield return new WaitForSeconds(aimingTime);
        Shoot();
        line.enabled = false;
        StartCoroutine(SetTransparency(0f, 1f, 0f, transform.GetChild(0).gameObject));
        yield return new WaitForSeconds(cooldownTime);

        startLoop = true;
    }

    protected override void BaseInitialization()
    {
        isSaberDangerous = true;
        transform.up = target.transform.position - transform.position;
        isJustBorn = true;
        isKilled = false;
        rend = GetComponent<Renderer>();
        line = transform.GetChild(1).gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        line.SetPosition(0, transform.localPosition);
        line.SetPosition(1, target.transform.position);
    }
}
