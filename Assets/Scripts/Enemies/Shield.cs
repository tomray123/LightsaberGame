using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Enemy
{
    private int defaultHP;
    private SpriteRenderer skin;
    private CircleCollider2D shieldCollider;
    private Droideka parent;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        BaseInitialization();
        skin = gameObject.GetComponent<SpriteRenderer>();
        shieldCollider = gameObject.GetComponent<CircleCollider2D>();
        if (transform.parent.gameObject.GetComponent<Droideka>())
        {
            parent = transform.parent.gameObject.GetComponent<Droideka>();
        }
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
    }

    protected override void DestroyWhenDead()
    {
        if (hp <= 0)
        {
            isKilled = true;
            skin.enabled = false;
            shieldCollider.enabled = false;
            StartCoroutine(RespawnShield(parameters[0].value));
        }
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
