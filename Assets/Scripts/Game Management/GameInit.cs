using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    [SerializeField]
    private bool resetToInitialSettings = false;
    [SerializeField]
    private int tutorialSceneBuildIndex = 2;
    [SerializeField]
    private int mainMenuSceneBuildIndex = 1;

    private ILevelsData levelsData;

    private void Awake()
    {
        levelsData = new LevelsDataPlayerPrefs();

        if (resetToInitialSettings)
        {
            PlayerPrefs.DeleteKey("init");
        }

        if (!PlayerPrefs.HasKey("init"))
        {
            // Setting basic controls.
            // Input type.
            PlayerPrefs.SetString("ControllerType", "NoJoystick");
            // Bullet correction type.
            PlayerPrefs.SetString("CorrectionType", "linear");
            // Smooth player's movement or not.
            PlayerPrefs.SetInt("SmoothSetting", 0);
            // Level number where player finished his game.
            levelsData.SetLastLevelNumber(0);

            // Setting records of each level in build to 0.
            for (int i = 1; i < SceneManager.sceneCountInBuildSettings - 3; i++)
            {
                levelsData.SetLevelRecord(i, 0);
                levelsData.SetStarsRate(i, 0);
            }
            PlayerPrefs.SetInt("init", 1);

            // Loading tutorial.
            SceneManager.LoadScene(tutorialSceneBuildIndex);
        }
        else
        {
            // Loading main menu.
            SceneManager.LoadScene(mainMenuSceneBuildIndex);
        }
    }
}
