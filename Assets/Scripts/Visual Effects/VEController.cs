using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VEController : MonoBehaviour, IPooledObject
{
    private SelfDestroyer destroyer;

    // Start is called before the first frame update
    void Awake()
    {
        destroyer = GetComponent<SelfDestroyer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnObjectSpawn()
    {
        if (destroyer != null)
        {
            
            StartCoroutine(destroyer.SelfDestroyToPool());
        }
    }

    public void OnObjectDestroy()
    {
        
    }
}
