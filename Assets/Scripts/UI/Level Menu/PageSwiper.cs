using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private float percentThreshold = 0.2f;
    [SerializeField]
    private float easing = 0.5f;
    [SerializeField]
    private int totalPages = 1;
    [SerializeField]
    private Canvas canvas;

    private Vector3 panelLocation;
    private int currentPage = 1;
    private Camera cam;
    private int localWidth;

    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.position;
        cam = Camera.main;
        localWidth = (int)(Screen.width / canvas.scaleFactor);
    }

    public void OnDrag(PointerEventData data)
    {
        Vector3 pressPosition = cam.ScreenToWorldPoint(data.pressPosition);
        Vector3 position = cam.ScreenToWorldPoint(data.position);
        float difference = pressPosition.x - position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        panelLocation = transform.position;
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += cam.ScreenToWorldPoint(new Vector3(-localWidth, 0, 0));
                print("Old location " + transform.position + " New location " + newLocation);  // REMOVE
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += cam.ScreenToWorldPoint(new Vector3(localWidth, 0, 0));
                print("Old location " + transform.position + " New location " + newLocation);  // REMOVE
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
