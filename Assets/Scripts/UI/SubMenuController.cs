using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuController : MonoBehaviour
{
    public GameObject currentMenu;
    public GameObject nextMenu;

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

    public void ChangeMenu()
    {
        currentMenu.SetActive(false);
        nextMenu.SetActive(true);
    }

}