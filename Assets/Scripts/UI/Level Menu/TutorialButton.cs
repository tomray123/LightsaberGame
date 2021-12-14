using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour, IAwakeInit
{
    [SerializeField]
    private int tutorialSceneIndex = 2;

    // Level Menu logic.
    private ISceneLoader sceneLoader;
    // Button component.
    private Button buttonComponent;

    public void OnAwakeInit()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        sceneLoader = new LevelSwitcher();
        // Getting button component.
        buttonComponent = GetComponent<Button>();
        SubscribeOnButtonEvent();
    }

    // Wrapper helps add LoadScene method as button onClick event listener.
    private void loadSceneWrapper()
    {
        sceneLoader.LoadScene(tutorialSceneIndex);
    }

    private void SubscribeOnButtonEvent()
    {
        buttonComponent.onClick.AddListener(loadSceneWrapper);
    }

    private void UnSubscribeOnButtonEvent()
    {
        buttonComponent.onClick.RemoveListener(loadSceneWrapper);
    }
}
