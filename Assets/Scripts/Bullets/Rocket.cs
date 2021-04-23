using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocket : MonoBehaviour
{
    public Vector3 targetLocation = Vector3.zero;

    public Vector3 reflectLocation = Vector3.zero;

    public float moveDuration = 1.0f;

    public float reflectedRocketSpeed = 1f;

    public Ease moveEaseForward = Ease.Linear;

    public Ease moveEaseBackward = Ease.Linear;

    public int damage = 300;

    public float damageRadius = 1f;

    public float tapRadius = 2f;

    public float reflectAreaRadius = 5f;

    public GameObject explosion;

    public SmartphoneInputController smartphoneInput;

    public MouseInputController mouseInput;

    protected Tween flyForwardAnimation;

    protected Tween flyBackwardAnimation;

    protected Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        smartphoneInput = SmartphoneInputController.instance;
        smartphoneInput.OnSingleTap += OnTap;
        mouseInput = MouseInputController.instance;
        mouseInput.OnSingleClick += OnClick;
        Vector2 direction = targetLocation - transform.position;
        transform.up = direction;
        rb = GetComponent<Rigidbody2D>();
        explosion.GetComponent<SimpleExplosion>().damage = damage;

        // Delete this
        LaunchRocket();
        reflectLocation = transform.position;
    }

    // Change this to OnDisable
    private void OnDestroy()
    {
        smartphoneInput.OnSingleTap -= OnTap;
        mouseInput.OnSingleClick -= OnClick;
    }

    public void OnTap()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(smartphoneInput.touch.position);
        clickPosition.z = 0;
        Vector2 offset = clickPosition - transform.position;
        Vector2 reflectArea = clickPosition - targetLocation;
        if (offset.magnitude <= tapRadius && reflectArea.magnitude < reflectAreaRadius)
        {
            ReverseRocket();
        }
    }

    public void OnClick()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;
        Vector2 offset = clickPosition - transform.position;
        Vector2 reflectArea = clickPosition - targetLocation;
        if (offset.magnitude <= tapRadius && reflectArea.magnitude < reflectAreaRadius)
        {
            ReverseRocket();
        }
    }

    public void LaunchRocket()
    {
        flyForwardAnimation = transform.DOMove(targetLocation, moveDuration).SetEase(moveEaseForward);
    }

    public void ReverseRocket()
    {
        if(flyForwardAnimation.IsPlaying())
        {
            flyForwardAnimation.Kill();
            transform.up = reflectLocation;
            rb.velocity = reflectLocation.normalized * reflectedRocketSpeed;
            //flyBackwardAnimation = transform.DOMove(reflectLocation, moveDuration).SetEase(moveEaseBackward);
        }
    }

    public void BlowUp()
    {
        
    }
}
