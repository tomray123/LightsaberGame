using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 0.1f;
    Vector3 direction;

    private void Start()
    {
        direction = transform.up;  //Change it later
    }
    void FixedUpdate()
    {
        transform.Translate(direction * speed, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Got it");
        ContactPoint2D contact = collision.GetContact(0);
        direction = Vector3.Reflect(direction, contact.normal);
        transform.up = direction;
    }
}
