using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooker : MonoBehaviour
{
    public Transform target;

    public PlayerController plController;

    // Start is called before the first frame update
    void Start()
    {
        plController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
        {
            //target.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            plController.targetMove = dir;
            //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
