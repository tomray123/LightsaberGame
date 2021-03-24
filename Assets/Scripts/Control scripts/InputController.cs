using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public PlayerController plController;

    public GameObject player;

    public GameObject throwIcon;

    public IconCooldown throwIconCooldown;

    public Vector3 targetVector;

    public float throwCooldownDuration = 5f;

    public float width;

    protected DoTweenController tweenController;

    protected Touch touch;

    protected int throwLayerMask;

    protected float touchDuration;

    private Slider throwIconMask;

    

    public void BaseInitialization()
    {
        Transform saber = player.transform.GetChild(0);
        tweenController = saber.GetComponent<DoTweenController>();
        throwLayerMask = 1 << 10;
        throwIconMask = throwIcon.GetComponent<Slider>();
        throwIconCooldown = throwIcon.GetComponent<IconCooldown>();
    }

    public string CheckInputSmartphone()
    {
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
            {
                if (touch.tapCount > 1)
                {
                    return "doubleTap";
                }
                else
                {
                    return "singleTap";
                }
            }
            else
            {
                return "continious";
            }

        }
        else
        {
            touchDuration = 0.0f;
            return "none";
        }
    }

    public void ThrowSaber(Vector3 touchPosition)
    {
        if (throwIconCooldown.isFinishedCooldown)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(touchPosition);
            RaycastHit2D throwHit;
            throwHit = Physics2D.Raycast(player.transform.position, targetPos, 20f, throwLayerMask);

            Vector3 throwTarget;

            if (throwHit.collider != null)
            {
                throwTarget = throwHit.point;
            }
            else
            {
                throwTarget = targetPos;
            }

            StartCoroutine(tweenController.DoThrowAndRotate(throwTarget));
            StartCoroutine(throwIconCooldown.StartCooldown(throwCooldownDuration, throwIconMask));
        }
    }

}
