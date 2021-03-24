using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused = false;

    public GameObject PauseMenu;

    public GameObject pauseButton;

    public GameObject joystick;

    Scene currentScene;

    int nextSceneIndex;

    int prevSceneIndex;

    int currSceneIndex;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        currSceneIndex = currentScene.buildIndex;
        nextSceneIndex = currentScene.buildIndex + 1;
        prevSceneIndex = currentScene.buildIndex - 1;
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        //joystick.SetActive(false);
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
    }

    public void BackToMenu()
    {
        PauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        SceneManager.LoadScene(0);
        Enemy.NumberOfKilledEnemies = 0;
        Spawner.TotalNumberOfEnemies = -1;
        BasePlayerSettings.isKilled = false;
    }

    public void NextLevel()
    {
        PauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        SceneManager.LoadScene(nextSceneIndex);
        Enemy.NumberOfKilledEnemies = 0;
        Spawner.TotalNumberOfEnemies = -1;
        BasePlayerSettings.isKilled = false;
    }

    public void PreviousLevel()
    {
        PauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        SceneManager.LoadScene(prevSceneIndex);
        Enemy.NumberOfKilledEnemies = 0;
        Spawner.TotalNumberOfEnemies = -1;
        BasePlayerSettings.isKilled = false;
    }

    public void ThisLevel()
    {
        PauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        SceneManager.LoadScene(currSceneIndex);
        Enemy.NumberOfKilledEnemies = 0;
        Spawner.TotalNumberOfEnemies = -1;
        BasePlayerSettings.isKilled = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void LoadLevelFromMenu(int LevelNumber)
    {
        SceneManager.LoadScene(LevelNumber);
    }

    public void LoadLevel(int LevelNumber)
    {
        PauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        SceneManager.LoadScene(LevelNumber);
        Enemy.NumberOfKilledEnemies = 0;
        Spawner.TotalNumberOfEnemies = -1;
        BasePlayerSettings.isKilled = false;
    }
}
