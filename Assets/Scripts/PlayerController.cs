using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 targetMove;

    // Update is called once per frame
    void Update()
    {
        transform.up = targetMove;
    }
}
