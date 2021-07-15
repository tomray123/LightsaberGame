using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    [SerializeField]
    private bool resetToInitialSettings = false;

    private void Awake()
    {
        if (resetToInitialSettings)
        {
            PlayerPrefs.DeleteKey("init");
        }

        if (!PlayerPrefs.HasKey("init"))
        {
            // Setting records of each level in build to 0.
            for (int i = 1; i < SceneManager.sceneCountInBuildSettings + 1; i++)
            {
                PlayerPrefs.SetInt("rec_lvl" + i.ToString(), 0);
            }
            PlayerPrefs.SetInt("init", 1);
        }
    }
}
