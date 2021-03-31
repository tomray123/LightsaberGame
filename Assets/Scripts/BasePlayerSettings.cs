using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerSettings : MonoBehaviour
{
    private Renderer rend;

    public HealthBar healthBar;

    public int maxHealth = 500;

    public int currentHealth;

    public static bool isKilled = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            currentHealth -= other.GetComponent<Bullet>().damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(ChangeColor());
            Destroy(other.gameObject);
        }
    }

    IEnumerator ChangeColor()
    {
        for (float i = 1f; i >= 0; i -= 0.05f)
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
        if (currentHealth <= 0)
        {
            isKilled = true;
            Destroy(gameObject);
        }
    }
}
