using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.15f;
    Vector3 direction;

    public int damage = 0;

    public float RayDelta = 10f;

    public float MaxRayLength = 10f;

    public GameObject frontPoint;
    public GameObject target;

    public bool isDangerous;

    private Rigidbody2D rb;

    private void Start()
    {

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
        /* //Layer mask for casting enemies
         int layerMask = 1 << 9;
         RaycastHit hit;

         Vector3 leftRayStart = transform.TransformPoint(-RayDelta, 0, 0);
         Vector3 rightRayStart = transform.TransformPoint(RayDelta, 0, 0);

         Debug.Log(leftRayStart);
         Debug.Log(transform.position);
         Debug.Log(rightRayStart);

         Ray leftRay = new Ray(leftRayStart, direction);
         Ray middleRay = new Ray(transform.position, direction);
         Ray rightRay = new Ray(rightRayStart, direction);

         Debug.Log(leftRay.origin);
         Debug.Log(middleRay.origin);
         Debug.Log(rightRay.origin);

         Physics.Raycast(leftRay, out hit, MaxRayLength, layerMask);
         Physics.Raycast(middleRay, out hit, MaxRayLength, layerMask);
         Physics.Raycast(rightRay, out hit, MaxRayLength, layerMask);



         Debug.DrawRay(leftRay.origin, direction * MaxRayLength, Color.yellow);
         Debug.DrawRay(middleRay.origin, direction * MaxRayLength, Color.yellow);
         Debug.DrawRay(rightRay.origin, direction * MaxRayLength, Color.yellow);*/
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LightSaber"))
        {
            direction = Vector3.Reflect(direction, other.transform.up);
            transform.position = other.GetContact(0).point;
            direction = TrajectoryСorrection(direction);
            transform.up = direction;
            rb.velocity = direction.normalized * speed;
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

        Ray leftRay = new Ray(leftRayStart, newDirection);
        Ray middleRay = new Ray(transform.position, newDirection);
        Ray rightRay = new Ray(rightRayStart, newDirection);

        hitLeft = Physics2D.Raycast(leftRayStart, newDirection, MaxRayLength, layerMask);
        hitMiddle = Physics2D.Raycast(transform.position, newDirection, MaxRayLength, layerMask);
        hitRight = Physics2D.Raycast(rightRayStart, newDirection, MaxRayLength, layerMask);

        /*
        Debug.DrawRay(leftRay.origin, newDirection * MaxRayLength, Color.yellow);
        Debug.DrawRay(middleRay.origin, newDirection * MaxRayLength, Color.yellow);
        Debug.DrawRay(rightRay.origin, newDirection * MaxRayLength, Color.yellow);
        */

        if (hitLeft.collider != null)
        {
            Debug.Log("Hit the enemy");
            newDirection = hitLeft.collider.transform.position - transform.position;
        }
        if (hitMiddle.collider != null)
        {
            Debug.Log("Hit the enemy");
            newDirection = hitMiddle.collider.transform.position - transform.position;
        }
        if (hitRight.collider != null)
        {
            Debug.Log("Hit the enemy");
            newDirection = hitRight.collider.transform.position - transform.position;
        }

        return newDirection;
    }

}
