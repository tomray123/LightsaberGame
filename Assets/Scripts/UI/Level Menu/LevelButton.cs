using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{   
    // Loads first level from pause menu.
    public void LoadLevel(Text levelNumber)
    {
        Time.timeScale = 1f;
        PauseController.IsGamePaused = false;

        SceneManager.LoadScene(int.Parse(levelNumber.text));

        // Reseting the counter of all enemies on level.
        Spawner.TotalNumberOfEnemies = -1;
        // Player is not killed.
        BasePlayerSettings.isKilled = false;
    }
}
