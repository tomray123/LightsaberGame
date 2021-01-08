using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 0.12f;
    Vector3 direction;

    private void Start()
    {
        direction = transform.up;  //Change it later
    }
    void FixedUpdate()
    {
        transform.Translate(direction * speed, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Got it");
            direction = Vector3.Reflect(direction, other.transform.up);
            transform.up = direction;
        }
    }
}
