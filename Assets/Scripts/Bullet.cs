using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.15f;
    Vector3 direction;

    public int damage = 0;

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
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LightSaber")) 
        {
            direction = Vector3.Reflect(direction, other.transform.up);
            transform.position = other.GetContact(0).point;
            transform.up = direction;
            rb.velocity = direction.normalized * speed;
        }
    }

}
