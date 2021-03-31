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
    public List<EnemyParameter> parameters = new List<EnemyParameter>(6) {new EnemyParameter(), new EnemyParameter(),
                                                                          new EnemyParameter(), new EnemyParameter(),
                                                                          new EnemyParameter(), new EnemyParameter() };

    public GameObject target;

    public float timeToFirstShoot = 0.8f;

    public float timeToShoot = 1f;

    public GameObject bullet;

    public int hp = 100;

    public int damage = 100;

    protected bool isTimeToShoot = true;

    protected bool isJustBorn = true;

    public bool isKilled = false;

    public static int NumberOfKilledEnemies = 0;

    private Renderer renderer;

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
        renderer = GetComponent<Renderer>();
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
            StartCoroutine(ChangeColor());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("LightSaber"))
        {
            hp -= other.GetComponent<SaberSettings>().damage;
            StartCoroutine(ChangeColor());
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Bullet>().isDangerous = true;
        }
    }

    protected IEnumerator ChangeColor()
    {
        for (float i = 1f; i >= 0; i-=0.05f)
        {
            Color cl = new Color(1, i, i);
            renderer.material.color = cl;
            yield return null;
        }
        for (float i = 0; i < 1f; i += 0.05f)
        {
            Color cl = new Color(1, i, i);
            renderer.material.color = cl;
            yield return null;
        }

        DestroyWhenDead();
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
