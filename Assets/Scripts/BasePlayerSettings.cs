using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerSettings : MonoBehaviour
{
    // For changing player's color.
    private Renderer rend;

    // UI health bar slider.
    public HealthBar healthBar;

    public int maxHealth = 500;

    public int currentHealth;

    // For checking whether palyer's killed or not.
    public static bool isKilled = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checking for bullet layer.
        if (other.gameObject.layer == 8)
        {
            // Getting damage.
            currentHealth -= other.GetComponent<Bullet>().damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(ChangeColor());
            // Destroying the bullet.
            Destroy(other.gameObject);
        }

        // Checking for explosion layer.
        if (other.gameObject.layer == 11)
        {
            // Getting damage.
            currentHealth -= other.GetComponent<SimpleExplosion>().damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(ChangeColor());
        }
    }

    // Changes the color of the object from normal to red and back 
    // and also checks whether an object is dead and destroys it.
    IEnumerator ChangeColor()
    {
        // Changing the color of the object from normal to red.
        for (float i = 1f; i >= 0; i -= 0.05f)
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
        // Checking for death.
        if (currentHealth <= 0)
        {
            // Destroying the object.
            isKilled = true;
            gameObject.SetActive(false);
        }
    }
}
