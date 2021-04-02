using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuController : MonoBehaviour
{
    public GameObject UiContainer;

    void DeactivateAllUi(GameObject container)
    {
        foreach (Transform child in container.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void ActivateAllUi(GameObject container)
    {
        foreach (Transform child in container.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void OpenSubMenu(GameObject SubMenu)
    {
        DeactivateAllUi(UiContainer);
        SubMenu.SetActive(true);
    }

    public void CloseSubMenu(GameObject SubMenu)
    {
        ActivateAllUi(UiContainer);
        SubMenu.SetActive(false);
    }

}