using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        if (resetToInitialSettings)
        {
            PlayerPrefs.DeleteKey("init");
        }

        if (!PlayerPrefs.HasKey("init"))
        {
            // Setting basic controls.
            PlayerPrefs.SetString("ControllerType", "FloatJoystick");
            PlayerPrefs.SetString("CorrectionType", "linear");
            PlayerPrefs.SetInt("SmoothSetting", 0);

            // Setting records of each level in build to 0.
            for (int i = 1; i < SceneManager.sceneCountInBuildSettings - 3; i++)
            {
                PlayerPrefs.SetInt("rec_lvl" + i.ToString(), 0);
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
