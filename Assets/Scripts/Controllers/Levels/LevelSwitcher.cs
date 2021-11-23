using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSwitcher : ISceneLoader
{
    public void LoadScene(int sceneIndex)
    {
        // Unpausing game.
        Time.timeScale = 1f;
        PauseController.IsGamePaused = false;

        SceneManager.LoadScene(sceneIndex);

        // Reseting the counter of all enemies on level.
        Spawner.TotalNumberOfEnemies = -1;
        // Player is not killed.
        BasePlayerSettings.isKilled = false;
    }

    // Loads level with index from text component (used for buttons).
    public void LoadScene(Text buttonText)
    {
        // Parsing scene name.
        int sceneIndex;
        if (!int.TryParse(buttonText.text, out sceneIndex))
        {
            Debug.LogError("Scene with name " + buttonText.text + " doesn't contain any index.");
            return;
        }
        // Loading level (and skipping Initial Scene, Main Menu and tutorial).
        LoadScene(sceneIndex + 2);
    }
}
