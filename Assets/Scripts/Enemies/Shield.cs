using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Enemy
{
    private int defaultHP;
    private SpriteRenderer skin;
    private CircleCollider2D shieldCollider;
    private Droideka parent;
    private Animator animator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        BaseInitialization();
    }

    public override void OnObjectSpawn()
    {
        hp = startHp;
        BaseInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void BaseInitialization()
    {
        isSaberDangerous = true;
        isJustBorn = true;
        isKilled = false;
        rend = GetComponent<Renderer>();
        defaultHP = hp;
        animator = GetComponent<Animator>();
        skin = gameObject.GetComponent<SpriteRenderer>();
        shieldCollider = gameObject.GetComponent<CircleCollider2D>();
        if (transform.parent.gameObject.GetComponent<Droideka>())
        {
            parent = transform.parent.gameObject.GetComponent<Droideka>();
        }

        // Disabling animation.
        animator.SetBool("isHit", false);
        skin.enabled = true;

        // Enabling shield.
        gameObject.SetActive(true);
        shieldCollider.enabled = true;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Just check for error.
        if (!gameObject.activeSelf)
        {
            return;
        }

        KillingObjects killer = other.GetComponent<KillingObjects>();

        // Check for bullet layer and if bullet can hit enemy.
        if (other.gameObject.layer == 8 && other.gameObject.GetComponent<Bullet>().isDangerous && killer != null)
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            // Getting damage.
            hp -= killer.damage;
            // Generating death action to score system.
            if (hp <= 0)
            {
                if (!ReferenceEquals(bullet.shooter, gameObject))
                {
                    killer.factor++;
                }
                ObjectDeath(killer.factor);
            }

            // Starting an animation.
            animator.SetBool("isHit", true);
            DestroyWhenDead();
            // Destroying the bullet.
            pool.ReturnToPool(other.gameObject);
        }

        // Checking for explosion layer.
        if (other.gameObject.layer == 11 && killer != null)
        {
            // Getting damage.
            hp -= killer.damage;
            // Generating death action to score system.
            if (hp <= 0)
            {
                ObjectDeath(killer.factor);
            }

            // Starting an animation.
            animator.SetBool("isHit", true);
            DestroyWhenDead();
        }

        // Checking for lightsaber.
        if (other.gameObject.CompareTag("LightSaber") && isSaberDangerous && killer != null)
        {
            // Getting damage.
            hp -= killer.damage;
            // Generating death action to score system.
            if (hp <= 0)
            {
                ObjectDeath(killer.factor);
            }
            // Starting a saber's non-hit cooldown.
            StartCoroutine(SaberDamageCooldown());

            // Starting an animation.
            animator.SetBool("isHit", true);
            DestroyWhenDead();
        }
    }

    public override void DestroyWhenDead()
    {
        if (hp <= 0)
        {
            isKilled = true;
            shieldCollider.enabled = false;
            StartCoroutine(RespawnShield(parameters[0].value));
        }
    }

    // This function is called by hit animation clip when animation ends.
    public void DisableSkin()
    {
        skin.enabled = false;
        // Disabling animation.
        animator.SetBool("isHit", false);
    }

    protected override IEnumerator SaberDamageCooldown()
    {
        isSaberDangerous = false;
        parent.isSaberDangerous = false;
        yield return new WaitForSeconds(saberDamageCooldown);
        isSaberDangerous = true;
        parent.isSaberDangerous = true;
    }

    protected IEnumerator RespawnShield(float respawnTime)
    {
        /*
        for (int i = 0; i < parent.droidekaCol.Length; i++)
        {
            parent.droidekaCol[i].enabled = true;
        }
        */
        parent.droidekaCol.enabled = true;

        yield return new WaitForSeconds(respawnTime);

        hp = defaultHP;
        isKilled = false;
        skin.enabled = true;
        shieldCollider.enabled = true;
        /*
        for (int i = 0; i < parent.droidekaCol.Length; i++)
        {
            parent.droidekaCol[i].enabled = false;
        }
        */
        parent.droidekaCol.enabled = false;
        yield return new WaitForSeconds(0.1f);

        skin.enabled = false;
        shieldCollider.enabled = false;
        /*for (int i = 0; i < parent.droidekaCol.Length; i++)
        {
            parent.droidekaCol[i].enabled = true;
        }*/
        yield return new WaitForSeconds(0.2f);

        skin.enabled = true;
        shieldCollider.enabled = true;
        /*for (int i = 0; i < parent.droidekaCol.Length; i++)
        {
            parent.droidekaCol[i].enabled = false;
        }*/
        yield return new WaitForSeconds(0.3f);

        skin.enabled = false;
        shieldCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);

        skin.enabled = true;
        shieldCollider.enabled = true;
    }
}
