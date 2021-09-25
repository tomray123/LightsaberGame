using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersController : MonoBehaviour
{
    ObjectPooler pool;

    private void Start()
    {
        pool = ObjectPooler.Instance;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Destroying all missing bullets
        if (other.gameObject.layer == 8)
        {
            pool.ReturnToPool(other.gameObject);
        }
    }
}
