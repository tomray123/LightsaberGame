using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Vector3 targetMove;

    public float rotationSpeed = 5f;

    public bool isSmooth = false;

    public Vector3 _targetLocation = Vector3.zero;

    void Update()
    {
        float angle = Angle360(Vector3.up, targetMove, Vector3.right);
        RotatePlayer(angle);

       
    }

    float Angle360(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) < 90f) ? 360f - angle : angle;
    }

    void RotatePlayer(float angle)
    {
        if (isSmooth)
        {
            //first way to rotate
            Quaternion targetQuater = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuater, rotationSpeed * Time.deltaTime);
        }
        else
        {
            //second way to rotate
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}