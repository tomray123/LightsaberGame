using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocketman : Enemy
{
    private LineRenderer leftLine;

    private LineRenderer rightLine;

    private Rocket rocket;

    // Forward animation type.
    [SerializeField]
    private Ease rocketPreparingAnimationEase = Ease.Linear;

    // Final position of the rocket before shot.
    private Vector3 launchPosition;

    // Initial spawn position of the rocket.
    private Vector3 rocketSpawnPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        BaseInitialization();
    }

    protected override void BaseInitialization()
    {
        leftLine = transform.GetChild(0).GetComponent<LineRenderer>();
        leftLine.enabled = false;
        rightLine = transform.GetChild(1).GetComponent<LineRenderer>();
        rightLine.enabled = false;
        isSaberDangerous = true;
        transform.up = target.transform.position - transform.position;
        isJustBorn = true;
        isKilled = false;
        rend = GetComponent<Renderer>();
        launchPosition = transform.GetChild(2).position;
        rocketSpawnPosition = transform.GetChild(3).position;
    }

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        BaseInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoop) StartCoroutine(EnemyLoop(parameters[0].value, parameters[1].value, parameters[2].value, parameters[3].value, parameters[4].value));
    }

    private IEnumerator EnemyLoop(float startLaserAngle, float endLaserAngle, float initTime, float aimingTime, float cooldownTime)
    {
        PrepareRocket();
        startLoop = false;

        if (isJustBorn)
        {
            isJustBorn = false;
            yield return new WaitForSeconds(initTime);
        }

        StartCoroutine(Aiming(-startLaserAngle, -endLaserAngle, aimingTime, leftLine, 200f));
        StartCoroutine(Aiming(startLaserAngle, endLaserAngle, aimingTime, rightLine, 200f));
        yield return new WaitForSeconds(aimingTime);

        rocket.LaunchRocket();

        yield return new WaitForSeconds(cooldownTime);

        startLoop = true;
    }

    protected IEnumerator Aiming(float startAngle, float endAngle, float duration, LineRenderer line, float length)
    {
        float timeElapsed = 0;
        float startX = line.GetPosition(0).x;
        float startY = line.GetPosition(0).y;
        startAngle = Mathf.Deg2Rad * startAngle;
        endAngle = Mathf.Deg2Rad * endAngle;
        float newAngle = startAngle;
        Vector3 endLinePosition = new Vector3(length * Mathf.Sin(newAngle) + startX, length * Mathf.Cos(newAngle) + startY, 0f);
        line.SetPosition(1, endLinePosition);
        line.enabled = true;

        while (timeElapsed < duration)
        {
            newAngle = Mathf.Lerp(startAngle, endAngle, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            endLinePosition.x = length * Mathf.Sin(newAngle) + startX;
            endLinePosition.y = length * Mathf.Cos(newAngle) + startY;
            line.SetPosition(1, endLinePosition);
            yield return null;
        }
        newAngle = endAngle;
        endLinePosition.x = length * Mathf.Sin(newAngle) + startX;
        endLinePosition.y = length * Mathf.Cos(newAngle) + startY;
        line.SetPosition(1, endLinePosition);
        line.enabled = false;
    }
    protected void PrepareRocket()
    {
        // Initially rocket is under rocketman.
        rocket = pool.SpawnFromPool(bullet, rocketSpawnPosition, Quaternion.identity).GetComponent<Rocket>();
        rocket.transform.parent = transform;
        // Then the rocket sticks its nose out of the muzzle.
        rocket.transform.DOMove(launchPosition, 0.5f).SetEase(rocketPreparingAnimationEase);
        rocket.damage = damage;
        rocket.targetLocation = target.transform.position;
    }

    protected override void Shoot()
    {
        
    }
}
