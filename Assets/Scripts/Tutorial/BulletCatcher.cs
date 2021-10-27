using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCatcher : MonoBehaviour
{
    private List<GameObject> bullets = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for bullet layer.
        if (collision.gameObject.layer == 8)
        {
            bullets.Add(collision.gameObject);
        }
    }

    public void ClearBullets()
    {
        foreach (GameObject b in bullets)
        {
            b.SetActive(false);
        }

        bullets.Clear();
    }

    public IEnumerator UnFreezeBullets(float duration)
    {
        float timeElapsed = 0;
        float i = 0f;

        if (bullets.Count > 0)
        {
            foreach (GameObject b in bullets)
            {
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                Bullet bullet = b.GetComponent<Bullet>();
                float endSpeed = bullet.speed;

                while (timeElapsed < duration)
                {
                    i = Mathf.Lerp(0f, endSpeed, timeElapsed / duration);
                    timeElapsed += Time.deltaTime;
                    rb.velocity = bullet.direction.normalized * i;
                    yield return null;
                }
                i = endSpeed;
                rb.velocity = bullet.direction.normalized * i;
            }
        }
    }

    public IEnumerator FreezeBullets(float duration)
    {
        float timeElapsed = 0;
        float i = 0f;

        if (bullets.Count > 0)
        {
            foreach (GameObject b in bullets)
            {
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                Bullet bullet = b.GetComponent<Bullet>();
                float startSpeed = bullet.speed;

                while (timeElapsed < duration)
                {
                    i = Mathf.Lerp(startSpeed, 0f, timeElapsed / duration);
                    timeElapsed += Time.deltaTime;
                    rb.velocity = bullet.direction.normalized * i;
                    yield return null;
                }
                i = 0f;
                rb.velocity = bullet.direction.normalized * i;
            } 
        }
    }
}
