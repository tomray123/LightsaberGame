using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInputController : MonoBehaviour
{
    public PlayerController plController;

    public GameObject player;

    public GameObject throwIcon;

    public IconCooldown throwIconCooldown;

    public Vector3 targetVector;

    public float throwCooldownDuration = 5f;

    public float width;

    protected DoTweenController tweenController;

    protected int throwLayerMask;

    private Slider throwIconMask;

    protected SmartphoneInputController smartphoneInput;

    public void BaseInitialization()
    {
        Transform saber = player.transform.GetChild(0);
        tweenController = saber.GetComponent<DoTweenController>();
        throwLayerMask = 1 << 10;
        throwIconMask = throwIcon.GetComponent<Slider>();
        throwIconCooldown = throwIcon.GetComponent<IconCooldown>();
        smartphoneInput = SmartphoneInputController.instance;
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
