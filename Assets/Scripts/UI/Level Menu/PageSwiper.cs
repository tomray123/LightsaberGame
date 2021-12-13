using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public float percentThreshold = 0.2f;
    [SerializeField]
    public float easing = 0.5f;
    [SerializeField]
    public int totalPages = 1;

    private Vector3 panelLocation;
    private int currentPage = 1;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.position;
        cam = Camera.main;
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
        /*float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }*/
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
