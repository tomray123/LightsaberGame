using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject NextLevelMenu;
    public GameObject GameUI;
    public GameObject PauseMenu;

    public GameObject joystick;
    public GameObject player;

    void Start()
    {
        switch (MenuController.whichController)
        {
            case "NoJoystick":

                joystick.GetComponent<Image>().enabled = false;
                joystick.transform.GetChild(0).GetComponent<Image>().enabled = false;
                joystick.GetComponent<MouseLooker>().enabled = true;
                joystick.GetComponent<JoystickController>().enabled = false;
                joystick.GetComponent<FloatJoystickController>().enabled = false;

                break;

            case "FloatJoystick":

                joystick.GetComponent<Image>().enabled = true;
                joystick.transform.GetChild(0).GetComponent<Image>().enabled = true;
                joystick.GetComponent<JoystickController>().enabled = false;
                joystick.GetComponent<FloatJoystickController>().enabled = true;
                joystick.GetComponent<MouseLooker>().enabled = false;

                break;

            case "ClassicJoystick":

                joystick.GetComponent<Image>().enabled = true;
                joystick.transform.GetChild(0).GetComponent<Image>().enabled = true;
                joystick.GetComponent<JoystickController>().enabled = true;
                joystick.GetComponent<FloatJoystickController>().enabled = false;
                joystick.GetComponent<MouseLooker>().enabled = false;

                break;
        }


        if (MenuController.isSmooth)
        {
            player.GetComponent<PlayerController>().isSmooth = true;
        }
        else
        {
            player.GetComponent<PlayerController>().isSmooth = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.NumberOfKilledEnemies == Spawner.TotalNumberOfEnemies)
        {
            PauseController.IsGamePaused = true;
            joystick.SetActive(false);
            Time.timeScale = 0f;
            GameUI.SetActive(false);
            PauseMenu.SetActive(false);
            NextLevelMenu.SetActive(true);
            Enemy.NumberOfKilledEnemies = 0;
            Spawner.TotalNumberOfEnemies = -1;
        }
    }
}
