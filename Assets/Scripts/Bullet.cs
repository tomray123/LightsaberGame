using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.15f;
    Vector3 direction;
    public GameObject frontPoint;

    //public float bit = 0.01f;
    //private bool timeToStop = false;

    private void Start()
    {
        direction = transform.up;  //Change it later
        Vector2[] colPoints = transform.GetComponent<EdgeCollider2D>().points;
        frontPoint.transform.localPosition = colPoints[0];
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
            transform.position = frontPoint.transform.position;
            transform.up = direction;
            
            //timeToStop = true;
        }
    }


    /*IEnumerator SmoothMove()
    {
        int i;
        for (i = 0; i < speed / bit; i++)
        {
            yield return new WaitForSeconds(Time.deltaTime / (speed / bit));
            transform.Translate(direction * bit * Time.deltaTime, Space.World);
            if (timeToStop)
            {
                break;
            }
        }
        if (timeToStop)
        {
            for (; i < speed / bit; i++)
            {
                transform.Translate(direction * bit * Time.deltaTime, Space.World);
            }
        }
        timeToStop = false;
        
    }*/
}
