using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 targetMove;

    public float rotationSpeed = 5f;

    public bool isSmooth = false;

    void Update()
    {
        float angle = Angle360(Vector3.up, targetMove, Vector3.right);

        if (isSmooth)
        {
            //third way to rotate
            Quaternion targetQuater = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuater, rotationSpeed * Time.deltaTime);
            
        }
        else
        {
            //first way to rotate
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //second way to rotate
        //Quaternion targetQuater = Quaternion.Euler(0, 0, angle);
        //transform.rotation  = Quaternion.Slerp(transform.rotation, targetQuater, Time.deltaTime * rotationSpeed);

        
    }

    float Angle360(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) < 90f) ? 360f - angle : angle;
    }

}
