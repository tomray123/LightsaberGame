using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagePositioner : MonoBehaviour, IAwakeInit, IStartInit
{
    private float pageWidth;
    private GameObject levelHolder;
    private float initialY;

    public void OnAwakeInit()
    {
        levelHolder = gameObject;
    }

    public void OnStartInit()
    {
        pageWidth = levelHolder.GetComponent<RectTransform>().rect.width;
        initialY = levelHolder.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y;
        SetRightPosition();
    }

    public void SetRightPosition()
    {
        for( int i = 1; i < levelHolder.transform.childCount; i++)
        {
            RectTransform page = levelHolder.transform.GetChild(i).GetComponent<RectTransform>();
            page.anchoredPosition = new Vector2(pageWidth * i, initialY);
        }
    }
}
