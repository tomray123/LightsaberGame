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

    public EnemyParameter(string _name = "none", float _value = 0)
    {
        name = _name;
        value = _value;
    }
}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public List<EnemyParameter> parameters = new List<EnemyParameter>(8) {new EnemyParameter(), new EnemyParameter(),
                                                                          new EnemyParameter(), new EnemyParameter(),
                                                                          new EnemyParameter(), new EnemyParameter(),
                                                                          new EnemyParameter(), new EnemyParameter() };

    public GameObject target;

    public float timeToFirstShoot = 0.8f;

    public float timeToShoot = 1f;

    public GameObject bullet;

    public int hp = 100;

    public int damage = 100;

    protected bool startLoop = true;

    protected bool isJustBorn = true;

    public bool isKilled = false;

    public static int NumberOfKilledEnemies = 0;

    protected Renderer rend;

    protected Transform shootIndicator;

    protected bool flash = false;

    void Start()
    {
        //BaseInitialization();
    }
   

    
    void Update()
    {
        //if (isTimeToShoot) StartCoroutine("Shoot");
    }
    

    protected void BaseInitialization()
    {
        transform.up = target.transform.position - transform.position;
        isJustBorn = true;
        isKilled = false;
        rend = GetComponent<Renderer>();
        shootIndicator = gameObject.transform.GetChild(0);
    }

    protected void Shoot(/*float time*/)
    {
        /*
        isTimeToShoot = false;
        if (isJustBorn)
        {
            isJustBorn = false;
            yield return new WaitForSeconds(timeToFirstShoot - 1.2f);

            for (int i = 0; i < 6; i++)
            {
                flash = !flash;
                shootIndicator.gameObject.SetActive(flash);
                yield return new WaitForSeconds(0.2f);
            }

        }*/

        GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.GetComponent<Bullet>().damage += damage;

        /*
        yield return new WaitForSeconds(time - 1.2f);
        for (int i = 0; i < 6; i++)
        {
            flash = !flash;
            shootIndicator.gameObject.SetActive(flash);
            yield return new WaitForSeconds(0.2f);
        }
        isTimeToShoot = true;*/
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        // Check for bullet layer and for danger
        if(other.gameObject.layer == 8 && other.gameObject.GetComponent<Bullet>().isDangerous)
        {
            hp -= other.GetComponent<Bullet>().damage;
            StartCoroutine(GetHit());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("LightSaber"))
        {
            hp -= other.GetComponent<SaberSettings>().damage;
            StartCoroutine(GetHit());
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Bullet>().isDangerous = true;
        }
    }

    protected virtual IEnumerator GetHit()
    {
        for (float i = 1f; i >= 0; i-=0.05f)
        {
            Color cl = new Color(1, i, i);
            rend.material.color = cl;
            yield return null;
        }
        for (float i = 0; i < 1f; i += 0.05f)
        {
            Color cl = new Color(1, i, i);
            rend.material.color = cl;
            yield return null;
        }
        DestroyWhenDead();
    }

    public IEnumerator SetTransparencyHigher(float duration, GameObject obj)
    {
        float timeElapsed = 0;
        float i = 0f;
        Color cl = new Color(1f, 1f, 1f, i);
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        while (timeElapsed < duration)
        {
            i = Mathf.Lerp(0, 0.7f, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            cl = new Color(1f, 1f, 1f, i);
            renderer.color = cl;
            yield return null;
        }
        i = 0.7f;
        cl = new Color(1f, 1f, 1f, i);
        renderer.color = cl;
    }
    public IEnumerator SetTransparencyLower(float duration, GameObject obj)
    {
        float timeElapsed = 0;
        float i = 0.7f;
        Color cl = new Color(1f, 1f, 1f, i);
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        while (timeElapsed < duration)
        {
            i = Mathf.Lerp(0.7f, 0, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            cl = new Color(1f, 1f, 1f, i);
            renderer.color = cl;
            yield return null;
        }
        i = 0;
        cl = new Color(1f, 1f, 1f, i);
        renderer.color = cl;
    }

    protected void DestroyWhenDead()
    {
        if (hp <= 0)
        {
            if (!isKilled)
            {
                NumberOfKilledEnemies++;
            }
            isKilled = true;
            Destroy(gameObject);
        }
    }
}
