using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocket : MonoBehaviour, IPooledObject
{
    public Vector3 targetLocation = Vector3.zero;

    public Vector3 reflectLocation = Vector3.zero;

    // public float moveDuration = 1.0f;

    public float rocketSpeed = 1f;

    // public float reflectedRocketSpeed = 1f;

    // public Ease moveEaseForward = Ease.Linear;

    // public Ease moveEaseBackward = Ease.Linear;

    public int damage = 300;

    public float damageRadius = 1f;

    public float tapRadius = 2f;

    public float reflectAreaRadius = 5f;

    public GameObject explosion;

    public SmartphoneInputController smartphoneInput;

    public MouseInputController mouseInput;

    // protected Tween flyForwardAnimation;

    // protected Tween flyBackwardAnimation;

    protected Rigidbody2D rb;

    public bool isDangerous;

    [HideInInspector]
    public ObjectPooler pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = ObjectPooler.Instance;
        smartphoneInput = SmartphoneInputController.instance;
        smartphoneInput.OnSingleTap += OnTap;
        mouseInput = MouseInputController.instance;
        mouseInput.OnSingleClick += OnClick;
        Vector2 direction = targetLocation - transform.position;
        transform.up = direction;
        rb = GetComponent<Rigidbody2D>();

        //Bullet doesn't hit enemy when bullet spawns
        isDangerous = false;

        reflectLocation = transform.position;
    }

    // Change this to OnDisable
    public void OnObjectDestroy()
    {
        smartphoneInput.OnSingleTap -= OnTap;
        mouseInput.OnSingleClick -= OnClick;
    }

    public void OnObjectSpawn() 
    {
        smartphoneInput = SmartphoneInputController.instance;
        smartphoneInput.OnSingleTap += OnTap;
        mouseInput = MouseInputController.instance;
        mouseInput.OnSingleClick += OnClick;
        Vector2 direction = targetLocation - transform.position;
        transform.up = direction;
        rb = GetComponent<Rigidbody2D>();

        //Bullet doesn't hit enemy when bullet spawns
        isDangerous = false;

        reflectLocation = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lighsaber can't reflect the rocket.
        if (!collision.gameObject.CompareTag("LightSaber"))
        {
            // Activating an explosion only if rocket is dangerous.
            if (isDangerous)
            {
                isDangerous = false;
                if (explosion != null)
                {
                    // Creating an explosion and destroying a rocket.
                    GameObject exp = pool.SpawnFromPool(explosion, transform.position, Quaternion.identity);
                    explosion.GetComponent<SimpleExplosion>().damage = damage;
                    pool.ReturnToPool(gameObject);
                }
                else
                {
                    Debug.LogWarning("No explosion attached to this GameObject.");
                }
            }
        }
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
        // flyForwardAnimation = transform.DOMove(targetLocation, moveDuration).SetEase(moveEaseForward);
        Vector2 shootDirection = targetLocation - transform.position;
        rb.velocity = shootDirection.normalized * rocketSpeed;
        // Uparenting rocket from rocketman.
        transform.parent = null;
    }

    public void ReverseRocket()
    {
        /*
        if(flyForwardAnimation.IsPlaying())
        {
            flyForwardAnimation.Kill();
           
            rb.velocity = reflectLocation.normalized * reflectedRocketSpeed;
            //flyBackwardAnimation = transform.DOMove(reflectLocation, moveDuration).SetEase(moveEaseBackward);
        }
        */
        transform.up = reflectLocation;
        rb.velocity = reflectLocation.normalized * rocketSpeed;
    }

    public void BlowUp()
    {
        
    }
}
