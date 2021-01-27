using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 targetMove;

    private Renderer renderer;

    public float rotationSpeed = 5f;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float angle = Angle360(Vector3.up, targetMove, Vector3.right);

        //first way to rotate
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //second way to rotate
        //Quaternion targetQuater = Quaternion.Euler(0, 0, angle);
        //transform.rotation  = Quaternion.Slerp(transform.rotation, targetQuater, Time.deltaTime * rotationSpeed);

        //third way to rotate
        Quaternion targetQuater = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuater, rotationSpeed * Time.deltaTime);
    }

    float Angle360(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) < 90f) ? 360f - angle : angle;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            StartCoroutine(ChangeColor());
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
    }
}
