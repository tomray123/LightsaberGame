using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenuController : MonoBehaviour
{
    public GameObject currentMenu;
    public GameObject nextMenu;

    SubMenuData curSubMenuData;
    SubMenuData nextSubMenuData;

    private ILevelsData levelsData;

    private void Start()
    {
        levelsData = new LevelsDataPlayerPrefs();

        if (currentMenu)
        {
            curSubMenuData = currentMenu.GetComponent<SubMenuData>();
        }
        else
        {
            Debug.LogWarning("No CurrentMenu Assigned!");
        }
        if (nextMenu)
        {
            nextSubMenuData = nextMenu.GetComponent<SubMenuData>();
        }
        else
        {
            Debug.LogWarning("No Next Menu Assigned!");
        }
    }

    public void ChangeMenu()
    {
        currentMenu.SetActive(false);
        nextMenu.SetActive(true);
        nextSubMenuData.whoOpenedMe = currentMenu;
    }

    public void GoBack()
    {
        currentMenu.SetActive(false);
        nextMenu = curSubMenuData.whoOpenedMe;
        curSubMenuData.whoOpenedMe = null;
        nextMenu.SetActive(true);
    }

    public void SwitchToDevMode()
    {
        int levelCount = SceneManager.sceneCountInBuildSettings - 4;
        // Level number where player finished his game.
        levelsData.SetLastLevelNumber(levelCount);
    }
}