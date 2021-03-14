using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Vector3 targetMove;

    public float rotationSpeed = 5f;

    public float ThrowTime = 5f;

    public bool isSmooth = false;

    public Transform saber;

    public bool isThrown;

    public Vector3 _targetLocation = Vector3.zero;

    Vector3 startPos;

    DoTweenController tweenController;

    private void Start()
    {
        saber = transform.GetChild(0);
        isThrown = false;
        startPos = saber.position;
        tweenController = saber.GetComponent<DoTweenController>();
    }

    void Update()
    {
        if (tweenController.isThrowTweenCompleted)
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

            if (Input.GetMouseButtonDown(1)) //Change this to (Input.touchCount > 1) in order to switch PC to mobile
            {
                StartCoroutine(tweenController.DoThrowAndRotate(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            }
        }
    }

    float Angle360(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) < 90f) ? 360f - angle : angle;
    }

    void ThrowSaber()
    {

        /*
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(targetPositionLocal);
        targetPosition.z = 0f;
        
        Vector3 direction = targetPosition - saber.position;
        Debug.DrawRay(saber.position, direction, Color.yellow);
        float startSpeed = speed;
        Vector3 startPosition = saber.position;

        while (isThrown)
        {
            
            speed = Mathf.Lerp(startSpeed, -1 * startSpeed, progress);
            progress += step;
            Debug.Log(speed);
            saber.position = Vector3.MoveTowards(saber.position, targetPosition*2, Time.deltaTime * speed);
            if (speed <= -1 * startSpeed)
            {
                speed = 0;
                progress = 0;
                isThrown = false;
            }
            yield return null;
            
            direction = targetPosition - saber.position;
            if (direction.magnitude < 2 && progress < 1)
            {
                speed = Mathf.Lerp(startSpeed, -1*startSpeed, progress);
                progress += step;
                Debug.Log(speed);
            }
            saber.position = Vector3.MoveTowards(saber.position, targetPosition, Time.deltaTime * speed);
            if (saber.position == targetPosition)
            {
                //speed = 0;
               // progress = 0;
                isThrown = false;
            }
            yield return null;
            
        }
        */
    }
        
}
