using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketman : Enemy
{
    public LineRenderer leftLine;

    public LineRenderer rightLine;

    public Rocket rocket;

    public Vector3 launchPosition;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoop) StartCoroutine(EnemyLoop(parameters[0].value, parameters[1].value, parameters[2].value, parameters[3].value, parameters[4].value));
    }

    private IEnumerator EnemyLoop(float startLaserAngle, float endLaserAngle, float initTime, float aimingTime, float cooldownTime)
    {
        startLoop = false;

        if (isJustBorn)
        {
            isJustBorn = false;
            yield return new WaitForSeconds(initTime);
        }

        PrepareRocket();

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
        rocket = Instantiate(bullet, launchPosition, Quaternion.identity).GetComponent<Rocket>();
        rocket.damage = damage;
    }

    protected override void Shoot()
    {
        
    }
}
