using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatJoystickController : MonoBehaviour
{
    public GameObject touchMarker;

    public Vector3 targetVector;

    public PlayerController plController;

    public float lowSpeed = 100f;

    public float medSpeed = 400f;

    public float highSpeed = 800f;

    public float width;

    public float height;

    private bool isMouseHeld = false;

    void Start()
    {
        touchMarker.transform.position = transform.position;
        width = gameObject.GetComponent<RectTransform>().rect.width;
        GetComponent<Image>().enabled = false;
        touchMarker.GetComponent<Image>().enabled = false;
    }

    void Update()
    {

        if (Input.GetMouseButton(0)) //Change this to (Input.touchCount > 0) in order to switch PC to mobile
        {
            Vector3 localTouchPos = Input.mousePosition; //Also change this to Input.GetTouch(0).position
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(localTouchPos);
            Vector3 localPos = Camera.main.WorldToScreenPoint(transform.position);

            touchPos.z = transform.position.z;
            localTouchPos.z = localPos.z;

            if (isMouseHeld == false)
            {

                GetComponent<Image>().enabled = true;
                touchMarker.GetComponent<Image>().enabled = true;
                transform.position = touchPos;
                isMouseHeld = true;
            }
            else
            {
                targetVector = touchPos - transform.position;
                plController.targetMove = targetVector;

                Vector3 localTargetPos = localTouchPos - localPos;

                if (localTargetPos.magnitude < width / 2)
                {

                    touchMarker.transform.position = touchPos;

                    if (localTargetPos.magnitude < 0.1f * width / 2)
                    {
                        plController.rotationSpeed = lowSpeed;
                    }
                    else if (localTargetPos.magnitude < 0.3f * width / 2)
                    {
                        plController.rotationSpeed = medSpeed;
                    }
                    else
                    {
                        plController.rotationSpeed = highSpeed;
                    }
                }
                else
                {
                    plController.rotationSpeed = highSpeed;
                    float newX = localPos.x + Mathf.Sin(Mathf.Atan2(localTargetPos.x, localTargetPos.y)) * width / 2;
                    float newY = localPos.y + Mathf.Cos(Mathf.Atan2(localTargetPos.x, localTargetPos.y)) * width / 2;
                    touchMarker.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(newX, newY, transform.position.z));
                }
            }
            
        }
        else
        {
            GetComponent<Image>().enabled = false;
            touchMarker.GetComponent<Image>().enabled = false;
            isMouseHeld = false;
            touchMarker.transform.position = transform.position;
            targetVector = new Vector3(0, 0, 0);
            plController.rotationSpeed = 0;
        }
    }
}
