using System;
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

    private GameSettingsControllerSO gameSettings = default;
    private GameSettingsSO defaultGameSettings;
    private ILevelsData levelsData;

    private void Awake()
    {
        levelsData = new LevelsDataPlayerPrefs();
        defaultGameSettings = Resources.Load<GameSettingsSO>("ScriptableObjects/Default Game Settings");
        gameSettings = Resources.Load<GameSettingsControllerSO>("ScriptableObjects/GameSettingsController");
        if (!defaultGameSettings)
        {
            Debug.LogError("Can't load Default Game Settings from VisualEffectsInit.");
            return;
        }

        if (resetToInitialSettings)
        {
            PlayerPrefs.DeleteKey("init");
        }

        if (!PlayerPrefs.HasKey("init"))
        {
            // Setting basic controls.
            // Input type.
            gameSettings.SetControllerType(defaultGameSettings.ControllerType);
            // Bullet correction type.
            gameSettings.SetCorrectionType(defaultGameSettings.CorrectionType);
            // Smooth player's movement or not.
            gameSettings.SetSmoothSetting(defaultGameSettings.IsSmooth);
            // Level number where player finished his game.
            levelsData.SetLastLevelNumber(0);
            // Visual effects are enabled by default.
            gameSettings.SetVisualEffects(defaultGameSettings.GraphicsEnabled);
            // Global volume is enabled by default.
            gameSettings.SetBoolPlayerPrefsData(PlayerPrefsParametersType.GlobalVolumeSetting, defaultGameSettings.GlobalVolumeOn);

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
