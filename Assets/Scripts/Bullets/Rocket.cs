using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocket : MonoBehaviour
{
    public Vector3 targetLocation = Vector3.zero;

    public Vector3 reflectLocation = Vector3.zero;

    public float moveDuration = 1.0f;

    public Ease moveEaseForward = Ease.Linear;

    public Ease moveEaseBackward = Ease.Linear;

    public float damageRadius = 1f;

    public float tapRadius = 2f;

    public float reflectAreaRadius = 5f;

    public SmartphoneInputController smartphoneInput;

    public MouseInputController mouseInput;

    protected Tween flyForwardAnimation;

    protected Tween flyBackwardAnimation;

    // Start is called before the first frame update
    void Start()
    {
        smartphoneInput = SmartphoneInputController.instance;
        smartphoneInput.OnSingleTap += OnTap;
        mouseInput = MouseInputController.instance;
        mouseInput.OnSingleClick += OnClick;
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

    // Update is called once per frame
    void Update()
    {
        
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
            flyBackwardAnimation = transform.DOMove(reflectLocation, moveDuration).SetEase(moveEaseBackward);
        }
    }

    public void BlowUp()
    {
        
    }
}
