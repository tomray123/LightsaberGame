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
    private Canvas canvas;

    private Vector3 lastPanelLocation;
    private RectTransform levelHolderTransform;
    private int totalPages = 1;
    private int currentPage = 1;
    private int localWidth;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        levelHolderTransform = GetComponent<RectTransform>();
        totalPages = transform.childCount;
        lastPanelLocation = levelHolderTransform.anchoredPosition;
        localWidth = (int)(Screen.width / canvas.scaleFactor);
    }

    public void OnDrag(PointerEventData data)
    {
        // Changing panel postion when dragging.
        Vector3 pressPosition = data.pressPosition;
        Vector3 position = data.position;
        float difference = pressPosition.x - position.x;
        levelHolderTransform.anchoredPosition = lastPanelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        // Calculating deviation percent.
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = lastPanelLocation;

            // Swiping to the next page.
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-localWidth, 0, 0);
            }
            // Swiping to the previous page.
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(localWidth, 0, 0);
            }
            // Starting smooth swiping.
            StartCoroutine(SmoothMove(levelHolderTransform.anchoredPosition, newLocation, easing));
            lastPanelLocation = newLocation;
        }
        else
        {
            // Returning to the current page.
            StartCoroutine(SmoothMove(levelHolderTransform.anchoredPosition, lastPanelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            levelHolderTransform.anchoredPosition = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
