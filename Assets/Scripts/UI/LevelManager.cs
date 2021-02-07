using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject NextLevelMenu;
    public GameObject GameUI;
    public GameObject PauseMenu;

    public GameObject joystick;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.isWin)
        {
            PauseController.IsGamePaused = true;
            joystick.SetActive(false);
            Time.timeScale = 0f;
            GameUI.SetActive(false);
            PauseMenu.SetActive(false);
            NextLevelMenu.SetActive(true);
            Enemy.isWin = false;
        }
    }
}
