using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float seconds = 0f;

    private ObjectPooler pool;

    private void Start()
    {
        pool = ObjectPooler.Instance;
    }

    public void SelfDestroy()
    {
        Destroy(gameObject, seconds);
    }

    public IEnumerator SelfDestroyToPool()
    {
        yield return new WaitForSeconds(seconds);
        pool.ReturnToPool(gameObject);
    }
}
