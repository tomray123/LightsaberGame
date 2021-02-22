using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerSettings : MonoBehaviour
{
    private Renderer renderer;

    public int hp = 500;

    public static bool isKilled = false;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            hp -= other.GetComponent<Bullet>().damage;
            StartCoroutine(ChangeColor());
            Destroy(other.gameObject);
        }
    }

    IEnumerator ChangeColor()
    {
        for (float i = 1f; i >= 0; i -= 0.05f)
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
        if (hp <= 0)
        {
            isKilled = true;
            Destroy(gameObject);
        }
    }
}
