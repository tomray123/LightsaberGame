using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, ILevelButton, IAwakeInit
{
    // GameObject containing level stars rate.
    [SerializeField]
    private GameObject starsUI;
    // GameObject containing shadow and lock UI.
    [SerializeField]
    private GameObject lockedStateUI;
    // Button which contains button component and text object with level number.
    [SerializeField]
    private GameObject button;

    // Level Menu logic.
    private ISceneLoader sceneLoader;

    // Button component.
    private Button buttonComponent;

    // Text component with level number.
    private Text levelNumText;

    // Stars gameobjects.
    private List<GameObject> stars = new List<GameObject>();

    private void OnDestroy()
    {
        UnSubscribeOnButtonEvent();
    }

    public void OnAwakeInit()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Getting text from button.
        levelNumText = button.transform.GetChild(0).GetComponent<Text>();
        if (!levelNumText)
        {
            Debug.LogError("Button " + button + " doesn't have text.");
            return;
        }

        // Getting button component.
        buttonComponent = button.GetComponent<Button>();

        // Getting stars.
        Transform starsHolder = starsUI.transform.GetChild(1);
        for (int i = 0; i < starsHolder.childCount; i++)
        {
            stars.Add(starsHolder.GetChild(i).gameObject);
        }

        sceneLoader = new LevelSwitcher();

        SubscribeOnButtonEvent();
    }

    // Wrapper helps add LoadScene method as button onClick event listener.
    private void loadSceneWrapper()
    {
        sceneLoader.LoadScene(levelNumText);
    }

    private void SubscribeOnButtonEvent()
    {
        buttonComponent.onClick.AddListener(loadSceneWrapper);
    }

    private void UnSubscribeOnButtonEvent()
    {
        buttonComponent.onClick.RemoveListener(loadSceneWrapper);
    }

    // Sets button opened state.
    public void OpenButton()
    {
        // Setting opened state.
        starsUI.SetActive(true);
        lockedStateUI.SetActive(false);
    }

    // Sets button closed state.
    public void CloseButton()
    {
        // Setting lock state.
        starsUI.SetActive(false);
        lockedStateUI.SetActive(true);
    }

    // Sets button rating with stars.
    public void SetButtonRate(int starsNumber = 1)
    {
        starsUI.SetActive(true);
        for (int i = 0; i < starsNumber; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
