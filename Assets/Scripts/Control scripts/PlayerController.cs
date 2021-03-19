using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Vector3 targetMove;

    public bool isPlayerMoving = false;

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

    private Vector3 throwTarget;

    private int throwLayerMask;

    private void Start()
    {
        saber = transform.GetChild(0);
        isThrown = false;
        startPos = saber.position;
        tweenController = saber.GetComponent<DoTweenController>();
        throwLayerMask = 1 << 10;
    }

    void Update()
    {
        float angle = Angle360(Vector3.up, targetMove, Vector3.right);
        RotatePlayer(angle);

        if (tweenController.isThrowTweenCompleted)
        {
            switch (GameSettings.device)
            {
                case GameSettings.Device.PC:

                    if (Input.GetMouseButtonDown(1)) //Change this to (Input.touchCount > 1) in order to switch PC to mobile
                    {
                        
                        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        RaycastHit2D throwHit;
                        throwHit = Physics2D.Raycast(transform.position, targetPos, 20f, throwLayerMask);
                        
                        if (throwHit.collider != null)
                        {
                            throwTarget = throwHit.point;
                        }
                        else
                        {
                            throwTarget = targetPos;
                        }
                        if (isPlayerMoving == true)
                        {
                            Vector3 throwDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                            angle = Angle360(Vector3.up, throwDirection, Vector3.right);
                            RotatePlayer(angle);
                        }
                        else
                        {
                            targetMove = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                        }
                        
                        StartCoroutine(tweenController.DoThrowAndRotate(throwTarget));
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

                        if (touch.phase == TouchPhase.Ended && touchDuration < 0.3f) //making sure it only check the touch once && it was a short touch/tap and not a dragging.
                            singleOrDouble(touch.position);

                    }
                    else
                        touchDuration = 0.0f;

                    break;
            }

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
        {
            if (isPlayerMoving == true)
            {
                Vector3 throwDirection = touchPosition - Camera.main.WorldToScreenPoint(transform.position);
                float angle = Angle360(Vector3.up, throwDirection, Vector3.right);
                RotatePlayer(angle);
            }
            else
            {
                targetMove = touchPosition - Camera.main.WorldToScreenPoint(transform.position);
            }
            

            Vector3 targetPos = Camera.main.ScreenToWorldPoint(touchPosition);
            RaycastHit2D throwHit;
            throwHit = Physics2D.Raycast(transform.position, targetPos, 20f, throwLayerMask);

            if (throwHit.collider != null)
            {
                throwTarget = throwHit.point;
            }
            else
            {
                throwTarget = targetPos;
            }
            StartCoroutine(tweenController.DoThrowAndRotate(throwTarget));
        }
    }

    void RotatePlayer(float angle)
    {
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
}