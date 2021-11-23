﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public GameObject WinMenu;
    public GameObject pauseButton;
    public GameObject PauseMenu;
    public GameObject LoseMenu;
    public GameObject background;

    Animator winMenuAnimator;
    Animator loseMenuAnimator;

    public Image HitVignette;

    public GameObject joystick;
    public GameObject player;

    private BasePlayerSettings basePlayerSettings;
    private WinMenuVisual wmVisual;

    [SerializeField]
    private float timeBreak = 1.5f;

    public string whichController;
    public bool isSmooth;

    // Singleton instance.
    public static LevelManager instance;

    private ScoreCounter scoreManager;

    private int oldRecord;
    private int levelNum;
    private string currentSceneRecordTag;
    private string currentSceneStarsTag;

    public void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }
        player = Instantiate(player, Vector3.zero, Quaternion.identity);
        player.GetComponent<PlayerVisualController>().HitVignette = HitVignette;
        basePlayerSettings = player.GetComponent<BasePlayerSettings>();
        basePlayerSettings.OnPlayerKilled += OnPlayerDeath;
    }

    void Start()
    {
        if (WinMenu)
        {
            winMenuAnimator = WinMenu.GetComponent<Animator>();
        }
        if (LoseMenu)
        {
            loseMenuAnimator = LoseMenu.GetComponent<Animator>();
        }
        wmVisual = WinMenu.GetComponent<WinMenuVisual>();
        Spawner.instance.onAllEnemiesDead += OnWin;
        scoreManager = ScoreCounter.instance;
        string sceneName = SceneManager.GetActiveScene().name;
        if (!int.TryParse(string.Join("", sceneName.Where(c => char.IsDigit(c))), out levelNum))
        {
            Debug.LogError("Can't get level number from scene with name: " + sceneName);
        }
        currentSceneRecordTag = "rec_lvl" + levelNum.ToString();
        currentSceneStarsTag = "stars_lvl" + levelNum.ToString();
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

    private void OnWin()
    {
        PauseController.IsGamePaused = true;
        //joystick.SetActive(false);

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //Time.timeScale = 0f;
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        pauseButton.SetActive(false);
        PauseMenu.SetActive(false);    
        background.SetActive(true);
        WinMenu.SetActive(true);
        winMenuAnimator.SetBool("isAppear", true);
        Spawner.TotalNumberOfEnemies = -1;
        BasePlayerSettings.isKilled = false;

        int currentScore = scoreManager.totalScore;

        if (currentScore >= scoreManager.scoreForThreeStars)
        {
            wmVisual.ActivateStars(3);
            PlayerPrefs.SetInt(currentSceneStarsTag, 3);
        }
        else if (currentScore >= scoreManager.scoreForTwoStars)
        {
            wmVisual.ActivateStars(2);
            PlayerPrefs.SetInt(currentSceneStarsTag, 2);
        }
        else
        {
            wmVisual.ActivateStars(1);
            PlayerPrefs.SetInt(currentSceneStarsTag, 1);
        }

        wmVisual.UpdateScoreText(currentScore);

        if (currentScore > oldRecord)
        {
            PlayerPrefs.SetInt(currentSceneRecordTag, currentScore);
        }
        else
        {
            wmVisual.ShowOldRecord(oldRecord);
        }

        int lastLevelNum = PlayerPrefs.GetInt("PlayerLastLevel");

        // Updating player's last completed level.
        if (lastLevelNum < levelNum)
        {
            PlayerPrefs.SetInt("PlayerLastLevel", levelNum);
        }
    }

    public void VisualizeNewRecord()
    {
        int currentScore = scoreManager.totalScore;

        if (currentScore > oldRecord)
        {
            // Visual for new record.
            wmVisual.EnableNewRecord();
        }
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(AfterDeath());
    }

    private IEnumerator AfterDeath()
    {
        yield return new WaitForSeconds(timeBreak);

        PauseController.IsGamePaused = true;
        //joystick.SetActive(false);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        PauseMenu.SetActive(false);
        background.SetActive(true);
        LoseMenu.SetActive(true);
        loseMenuAnimator.SetBool("isAppear", true);
        Spawner.TotalNumberOfEnemies = -1;
        BasePlayerSettings.isKilled = false;
        basePlayerSettings.OnPlayerKilled -= OnPlayerDeath;
    }

    private void OnDestroy()
    {
        basePlayerSettings.OnPlayerKilled -= OnPlayerDeath;
        Spawner.instance.onAllEnemiesDead -= OnWin;
    }
}
