using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : KillingObjects
{
    public float speed = 7f;

    int reflectCount = 0;

    [HideInInspector]
    public GameObject shooter = null;

    Vector3 direction;

    public float RayDelta = 10f;

    public float MaxRayLength = 10f;

    public float rayAngle = 6f;

    public string whichCorrection;

    public GameObject frontPoint;

    public GameObject target;

    public bool isDangerous;

    protected Rigidbody2D rb;

    private void Start()
    {
        reflectCount = 0;
        whichCorrection = PlayerPrefs.GetString("CorrectionType", "linear");

        direction = target.transform.position - transform.position;
        transform.up = direction;

        Vector2[] colPoints = transform.GetComponent<EdgeCollider2D>().points;
        frontPoint.transform.localPosition = colPoints[0];

        rb = GetComponent<Rigidbody2D>();

        rb.velocity = direction.normalized * speed;

        //Bullet doesn't hit enemy when bullet spawns
        isDangerous = false;
    }

    void FixedUpdate()
    {
        /*
        Debug.DrawRay(transform.position, direction.normalized * MaxRayLength, Color.yellow);
        Debug.DrawRay(transform.position, rb.velocity, Color.red);

        //Layer mask for casting enemies
        int layerMask = 1 << 9;
        RaycastHit hit;

        Vector3 leftRayStart =  transform.TransformPoint(-RayDelta, 0, 0);
        Vector3 rightRayStart = transform.TransformPoint(RayDelta, 0, 0);
        Vector3 leftRayDirection = direction;
        Vector3 rightRayDirection = direction;

        if (whichCorrection == "angular")
        {
            leftRayStart = transform.position;
            rightRayStart = transform.position;
            leftRayDirection = Quaternion.AngleAxis(rayAngle, Vector3.forward) * direction;
            rightRayDirection = Quaternion.AngleAxis(-1*rayAngle, Vector3.forward) * direction;
        }
        Debug.Log(leftRayStart);
        Debug.Log(transform.position);
        Debug.Log(rightRayStart);

        Ray leftRay = new Ray(leftRayStart, leftRayDirection);
        Ray middleRay = new Ray(transform.position, direction);
        Ray rightRay = new Ray(rightRayStart, rightRayDirection);

        Debug.Log(leftRay.origin);
        Debug.Log(middleRay.origin);
        Debug.Log(rightRay.origin);

        Physics.Raycast(leftRay, out hit, MaxRayLength, layerMask);
        Physics.Raycast(middleRay, out hit, MaxRayLength, layerMask);
        Physics.Raycast(rightRay, out hit, MaxRayLength, layerMask);

        Debug.DrawRay(leftRay.origin, leftRayDirection * MaxRayLength, Color.yellow);
        Debug.DrawRay(middleRay.origin, direction * MaxRayLength, Color.yellow);
        Debug.DrawRay(rightRay.origin, rightRayDirection * MaxRayLength, Color.yellow);
        */
    }
    void Update()
    {
        // Trajectory correction after reflection
        float angle = Vector3.Angle(rb.velocity, transform.up);
        if (angle > 1f || angle < -1f)
        {
            rb.velocity = transform.up * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LightSaber"))
        {
            reflectCount++;
            if (reflectCount > 1)
            {
                factor++;
            }
            direction = Vector3.Reflect(direction, other.transform.up);
            transform.position = other.GetContact(0).point;
            direction = TrajectoryСorrection(direction);
            transform.up = direction.normalized;
            rb.velocity = transform.up * speed;
        }
    }

    protected Vector3 TrajectoryСorrection(Vector3 newDirection)
    {
        //Layer mask for casting enemies
        int layerMask = 1 << 9;

        RaycastHit2D hitLeft;
        RaycastHit2D hitMiddle;
        RaycastHit2D hitRight;

        Vector3 leftRayStart = transform.TransformPoint(-RayDelta, 0, 0);
        Vector3 rightRayStart = transform.TransformPoint(RayDelta, 0, 0);
        Vector3 leftRayDirection = newDirection;
        Vector3 rightRayDirection = newDirection;

        if (whichCorrection == "angular")
        {
            leftRayStart = transform.position;
            rightRayStart = transform.position;
            leftRayDirection = Quaternion.AngleAxis(rayAngle, Vector3.forward) * newDirection;
            rightRayDirection = Quaternion.AngleAxis(-1 * rayAngle, Vector3.forward) * newDirection;
        }

        /*
        Ray leftRay = new Ray(leftRayStart, leftRayDirection);
        Ray middleRay = new Ray(transform.position, newDirection);
        Ray rightRay = new Ray(rightRayStart, rightRayDirection);

        Debug.DrawRay(leftRay.origin, newDirection * MaxRayLength, Color.yellow);
        Debug.DrawRay(middleRay.origin, newDirection * MaxRayLength, Color.yellow);
        Debug.DrawRay(rightRay.origin, newDirection * MaxRayLength, Color.yellow);
        */

        hitLeft = Physics2D.Raycast(leftRayStart, leftRayDirection, MaxRayLength, layerMask);
        hitMiddle = Physics2D.Raycast(transform.position, newDirection, MaxRayLength, layerMask);
        hitRight = Physics2D.Raycast(rightRayStart, rightRayDirection, MaxRayLength, layerMask);

        if (hitLeft.collider != null)
        {
            newDirection = hitLeft.collider.transform.position - transform.position;
        }
        if (hitMiddle.collider != null)
        {
            newDirection = hitMiddle.collider.transform.position - transform.position;
        }
        if (hitRight.collider != null)
        {
            newDirection = hitRight.collider.transform.position - transform.position;
        }

        return newDirection;
    }
}
