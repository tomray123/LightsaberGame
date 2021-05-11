using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInputController : MonoBehaviour
{
    public GameObject player;

    public PlayerController plController;

    public GameObject throwIcon;

    // Rotation direction of the character.
    public Vector3 targetVector;

    // Animation of icon's cooldown.
    public IconCooldown throwIconCooldown;

    public float throwCooldownDuration = 5f;

    // Tween animations controller.
    protected DoTweenController tweenController;

    protected int throwLayerMask;

    private Slider throwIconMask;

    protected SmartphoneInputController smartphoneInput;

    protected MouseInputController mouseInput;

    public void BaseInitialization()
    {
        Transform saber = player.transform.GetChild(0);
        tweenController = saber.GetComponent<DoTweenController>();
        // Layer mask for raycasting throw border collider.
        throwLayerMask = 1 << 10;
        throwIconMask = throwIcon.GetComponent<Slider>();
        throwIconCooldown = throwIcon.GetComponent<IconCooldown>();
        smartphoneInput = SmartphoneInputController.instance;
        mouseInput = MouseInputController.instance;
    }

    public void ThrowSaber(Vector3 touchPosition)
    {
        if (throwIconCooldown.isFinishedCooldown)
        {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(touchPosition);
            RaycastHit2D throwHit;
            throwHit = Physics2D.Raycast(player.transform.position, targetPos, 20f, throwLayerMask);
            Vector3 throwTarget;

            // Checking for throw border.
            if (throwHit.collider != null)
            {
                throwTarget = throwHit.point;
            }
            else
            {
                throwTarget = targetPos;
            }

            // Starting saber animation.
            StartCoroutine(tweenController.DoThrowAndRotate(throwTarget));
            // Starting icon animation.
            StartCoroutine(throwIconCooldown.StartCooldown(throwCooldownDuration, throwIconMask));
        }
    }

}
