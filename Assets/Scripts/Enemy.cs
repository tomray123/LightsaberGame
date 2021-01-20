using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public float timeToShoot = 1f;
    public GameObject bullet;
    private bool isTimeToShoot = true;

    void Start()
    {
        transform.up = target.transform.position - transform.position;
    }

    void Update()
    {
        if(isTimeToShoot) StartCoroutine(Shoot(timeToShoot));
    }

    private IEnumerator Shoot(float time)
    {
        Instantiate(bullet, transform.position, transform.rotation);
        isTimeToShoot = false;
        yield return new WaitForSeconds(time);
        isTimeToShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for bullet layer and for danger
        if(other.gameObject.layer == 8 && other.gameObject.GetComponent<Bullet>().isDangerous)
        {
            Debug.Log("Enemy was shot");
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Bullet>().isDangerous = true;
        }
    }
}
