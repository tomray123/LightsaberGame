using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooker : MonoBehaviour
{
    public float speed = 800f;

    public GameObject player;

    public PlayerController plController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameSettings.device)
        {
            case GameSettings.Device.PC:

                if (Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
                {
                    plController.isPlayerMoving = true;
                    var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
                    plController.targetMove = dir;
                    plController.rotationSpeed = speed;
                    //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    plController.isPlayerMoving = false;
                }

                break;

            case GameSettings.Device.Smartphone:

                if (Input.touchCount > 0) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
                {
                    plController.isPlayerMoving = true;
                    Vector3 tapPosition = Input.GetTouch(0).position;
                    var dir = tapPosition - Camera.main.WorldToScreenPoint(player.transform.position);
                    plController.targetMove = dir;
                    plController.rotationSpeed = speed;
                    //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    plController.isPlayerMoving = false;
                }

                break;
        }
    }
}
