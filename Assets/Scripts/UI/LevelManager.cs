using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject WinMenu;
    public GameObject pauseButton;
    public GameObject PauseMenu;
    public GameObject LoseMenu;

    public GameObject joystick;
    public GameObject player;

    public string whichController;
    public bool isSmooth;

    // Singleton instance.
    public static LevelManager instance;

    private int oldRecord;
    private string currentSceneRecordTag;

    public void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }
        player = Instantiate(player, Vector3.zero, Quaternion.identity);
    }

    void Start()
    {
        currentSceneRecordTag = "rec_lvl" + SceneManager.GetActiveScene().buildIndex;
        oldRecord = PlayerPrefs.GetInt(currentSceneRecordTag);
        whichController = PlayerPrefs.GetString("ControllerType", "NoJoystick");
        isSmooth = Convert.ToBoolean(PlayerPrefs.GetInt("SmoothSetting", 0));
        switch (whichController)
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


        if (isSmooth)
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
            //joystick.SetActive(false);
            Time.timeScale = 0f;
            pauseButton.SetActive(false);
            PauseMenu.SetActive(false);
            WinMenu.SetActive(true);
            Enemy.NumberOfKilledEnemies = 0;
            Spawner.TotalNumberOfEnemies = -1;
            BasePlayerSettings.isKilled = false;

            int currentScore = ScoreCounter.instance.totalScore;

            if (currentScore > oldRecord)
            {
                PlayerPrefs.SetInt(currentSceneRecordTag, currentScore);
            }
        }
        if (BasePlayerSettings.isKilled)
        {
            PauseController.IsGamePaused = true;
            //joystick.SetActive(false);
            Time.timeScale = 0f;
            pauseButton.SetActive(false);
            PauseMenu.SetActive(false);
            LoseMenu.SetActive(true);
            Enemy.NumberOfKilledEnemies = 0;
            Spawner.TotalNumberOfEnemies = -1;
            BasePlayerSettings.isKilled = false;
        }
    }
}
