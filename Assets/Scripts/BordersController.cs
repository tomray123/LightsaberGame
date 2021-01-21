using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        // Destroying all missing bullets
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
        }
    }
}
