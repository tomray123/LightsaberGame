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

    // Next scene number.
    int nextSceneIndex;

    // Previous scene number.
    int prevSceneIndex;

    // Currnet scene number.
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
        // Freezing the time.
        Time.timeScale = 0f;
        // Enabling pause menu.
        PauseMenu.SetActive(true);
        // Disabling pause button.
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        // Disabling pause menu.
        PauseMenu.SetActive(false);
        // Enabling pause button.
        pauseButton.SetActive(true);
        // Resume the time.
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
    }

    public void BackToMenu()
    {
        // Disabling pause menu.
        PauseMenu.SetActive(false);
        // Enabling pause button.
        pauseButton.SetActive(true);
        // Resume the time.
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        // Loading main menu.
        SceneManager.LoadScene(0);

        // Reseting the counter of killed enemies.
        Enemy.NumberOfKilledEnemies = 0;
        // Reseting the counter of all enemies on level.
        Spawner.TotalNumberOfEnemies = -1;
        // Player is not killed.
        BasePlayerSettings.isKilled = false;
    }

    public void NextLevel()
    {
        // Disabling pause menu.
        PauseMenu.SetActive(false);
        // Enabling pause button.
        pauseButton.SetActive(true);
        // Resume the time.
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        // Loading next level.
        SceneManager.LoadScene(nextSceneIndex);

        // Reseting the counter of killed enemies.
        Enemy.NumberOfKilledEnemies = 0;
        // Reseting the counter of all enemies on level.
        Spawner.TotalNumberOfEnemies = -1;
        // Player is not killed.
        BasePlayerSettings.isKilled = false;
    }

    public void PreviousLevel()
    {
        // Disabling pause menu.
        PauseMenu.SetActive(false);
        // Enabling pause button.
        pauseButton.SetActive(true);
        // Resume the time.
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        // Loading previous level.
        SceneManager.LoadScene(prevSceneIndex);

        // Reseting the counter of killed enemies.
        Enemy.NumberOfKilledEnemies = 0;
        // Reseting the counter of all enemies on level.
        Spawner.TotalNumberOfEnemies = -1;
        // Player is not killed.
        BasePlayerSettings.isKilled = false;
    }

    public void ThisLevel()
    {
        // Disabling pause menu.
        PauseMenu.SetActive(false);
        // Enabling pause button.
        pauseButton.SetActive(true);
        // Resume the time.
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        // Reloading current level.
        SceneManager.LoadScene(currSceneIndex);

        // Reseting the counter of killed enemies.
        Enemy.NumberOfKilledEnemies = 0;
        // Reseting the counter of all enemies on level.
        Spawner.TotalNumberOfEnemies = -1;
        // Player is not killed.
        BasePlayerSettings.isKilled = false;
    }

    // Loads first level from main menu.
    public void StartGame()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Loads any level from main menu.
    public void LoadLevelFromMenu(int LevelNumber)
    {
        SceneManager.LoadScene(LevelNumber);
    }

    // Loads first level from pause menu.
    public void LoadLevel(int LevelNumber)
    {
        // Disabling pause menu.
        PauseMenu.SetActive(false);
        // Enabling pause button.
        pauseButton.SetActive(true);
        // Resume the time.
        Time.timeScale = 1f;
        IsGamePaused = false;
        //joystick.SetActive(true);
        // Loading previous level.
        SceneManager.LoadScene(LevelNumber);

        // Reseting the counter of killed enemies.
        Enemy.NumberOfKilledEnemies = 0;
        // Reseting the counter of all enemies on level.
        Spawner.TotalNumberOfEnemies = -1;
        // Player is not killed.
        BasePlayerSettings.isKilled = false;
    }
}
