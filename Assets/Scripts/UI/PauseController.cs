using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused = false;

    public GameObject PauseMenu;

    public GameObject GameUI;

    public GameObject joystick;

    public void PauseGame()
    {
        IsGamePaused = true;
        joystick.SetActive(false);
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        GameUI.SetActive(false);
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        joystick.SetActive(true);
    }

    public void BackToMenu(string sceneName)
    {
        PauseMenu.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        joystick.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

    public void changeLevel(string levelName)
    {
        PauseMenu.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
        IsGamePaused = false;
        joystick.SetActive(true);
        SceneManager.LoadScene(levelName);
    }
}
