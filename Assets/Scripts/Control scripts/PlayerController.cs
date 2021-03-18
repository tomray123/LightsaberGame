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

    private float touchDuration;

    private Touch touch;

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
            switch (GameSettings.device)
            {
                case GameSettings.Device.PC:

                    if (Input.GetMouseButtonDown(1)) //Change this to (Input.touchCount > 1) in order to switch PC to mobile
                    {
                        //RaycastHit2D throwHit;
                        //throwHit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 5f);
                        //Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) * 5f, Color.yellow);
                        //targetMove = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                        StartCoroutine(tweenController.DoThrowAndRotate(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                    }

                    break;

                case GameSettings.Device.Smartphone:

                    if (Input.touchCount > 0)
                    {
                        touch = Input.GetTouch(0);
                        touchDuration += Time.deltaTime;

                        if (Input.touchCount > 1)
                        {
                            touch = Input.GetTouch(1);
                            if (touch.phase == TouchPhase.Began)
                                touchDuration = 0.0f;
                            touchDuration += Time.deltaTime;
                        }

                        /*
                        Vector3 targetPosition = touch.position;
                        targetMove = targetPosition - Camera.main.WorldToScreenPoint(transform.position);
                        */

                        if (touch.phase == TouchPhase.Ended && touchDuration < 0.3f) //making sure it only check the touch once && it was a short touch/tap and not a dragging.
                            singleOrDouble(touch.position);

                    }
                    else
                        touchDuration = 0.0f;

                    break;
            }

        }

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
    }

    float Angle360(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) < 90f) ? 360f - angle : angle;
    }

    void singleOrDouble(Vector3 touchPosition)
    {
        if (touch.tapCount > 1)
            StartCoroutine(tweenController.DoThrowAndRotate(Camera.main.ScreenToWorldPoint(touchPosition)));
    }
}