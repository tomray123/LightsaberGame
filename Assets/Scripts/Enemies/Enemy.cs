using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class EnemyParameter
{
    [Header("Parameter name")]
    public string name;
    [Header("Parameter value")]
    public float value;

    // Constructor for enemy parameter.
    public EnemyParameter(string _name = "none", float _value = 0)
    {
        name = _name;
        value = _value;
    }
}

// Base enemy class.
public class Enemy : ScorableObjects
{
    // List of enemy parameters.
    [SerializeField]
    public List<EnemyParameter> parameters = new List<EnemyParameter>(8) {new EnemyParameter(), new EnemyParameter(),
                                                                          new EnemyParameter(), new EnemyParameter(),
                                                                          new EnemyParameter(), new EnemyParameter(),
                                                                          new EnemyParameter(), new EnemyParameter() };

    // Enemie's target.
    public GameObject target;

    public Action<Enemy, int> OnSpawnObjectDeath;

    public float timeToFirstShoot = 0.8f;

    // Time interval between shots (not for all enemies).
    public float timeToShoot = 1f;

    public GameObject bullet;

    public int hp = 100;

    public int damage = 100;

    // Intended to define the position in spawn list.
    public int spawnIndex = 0;

    // Sets the time when enemy can't get damage from player's saber after first saber's hit. 
    public float saberDamageCooldown = 3f;

    // Defines whether the saber can hit enemy or not.
    [HideInInspector]
    public bool isSaberDangerous = true;

    // Defines whether the enemy loop is started.
    protected bool startLoop = true;

    // Defines whether the enemy was just spawned and haven't made any shot yet.
    protected bool isJustBorn = true;

    [HideInInspector]
    public bool isKilled = false;

    protected Renderer rend;

    // Flashes over the enemy.
    protected Transform shootIndicator;

    protected bool flash = false;

    protected override void Start()
    {
        base.Start();
    }

    public void EnemyDestroy()
    {
        if (OnSpawnObjectDeath != null)
        {
            OnSpawnObjectDeath(this, spawnIndex);
        }
        spawnIndex = 0;
    }

    // Usually called from Start() method.
    protected virtual void BaseInitialization()
    {
        isSaberDangerous = true;
        transform.up = target.transform.position - transform.position;
        isJustBorn = true;
        isKilled = false;
        rend = GetComponent<Renderer>();
        // Indicator must be a first child of enemy.
        shootIndicator = gameObject.transform.GetChild(0);
    }

    // Simple shoot method.
    protected virtual void Shoot()
    {
        // Creating a bullet and setting its damage.
        Bullet bulletClone = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
        bulletClone.damage += damage;
        bulletClone.shooter = gameObject;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
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
            StartCoroutine(GetHit());
            // Destroying the bullet.
            Destroy(other.gameObject);
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
            StartCoroutine(GetHit());
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
            StartCoroutine(GetHit());
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        // Checking for bullet layer.
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Bullet>().isDangerous = true;
        }
        // Checking for rocket layer.
        if (other.gameObject.layer == 12)
        {
            other.gameObject.GetComponent<Rocket>().isDangerous = true;
        }
    }

    // Saber's cooldown.
    protected virtual IEnumerator SaberDamageCooldown()
    {
        isSaberDangerous = false;
        yield return new WaitForSeconds(saberDamageCooldown);
        isSaberDangerous = true;
    }

    // Changes the color of the object from normal to red and back 
    // and also checks whether an object is dead and destroys it.
    protected virtual IEnumerator GetHit()
    {
        // Changing the color of the object from normal to red.
        for (float i = 1f; i >= 0; i-=0.05f)
        {
            Color cl = new Color(1, i, i);
            rend.material.color = cl;
            yield return null;
        }
        // Changing the color of the object from red to normal.
        for (float i = 0; i < 1f; i += 0.05f)
        {
            Color cl = new Color(1, i, i);
            rend.material.color = cl;
            yield return null;
        }
        // Checks whether an object is dead and destroys it.
        DestroyWhenDead();
    }

    // Sets the transparency of object's sprite.
    public IEnumerator SetTransparency(float duration, float startValue, float EndValue, GameObject obj)
    {
        float timeElapsed = 0;
        float i = 0f;
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        float r = renderer.color.r;
        float g = renderer.color.g;
        float b = renderer.color.b;
        Color cl = new Color(r, g, b, i);
        while (timeElapsed < duration)
        {
            i = Mathf.Lerp(startValue, EndValue, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            cl = new Color(r, g, b, i);
            renderer.color = cl;
            yield return null;
        }
        i = EndValue;
        cl = new Color(r, g, b, i);
        renderer.color = cl;
    }

    // Checks whether an object is dead and destroys it.
    protected virtual void DestroyWhenDead()
    {
        if (hp <= 0)
        {
            EnemyDestroy();
            isKilled = true;
            Destroy(gameObject);
        }
    }
}
