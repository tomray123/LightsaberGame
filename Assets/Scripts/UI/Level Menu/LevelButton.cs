using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
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
    [SerializeField]
    private ISceneLoader sceneLoader;

    // Button component.
    private Button buttonComponent;

    // Text component with level number.
    private Text levelNumText;

    private void Awake()
    {
        InitializeData();
    }

    private void OnDestroy()
    {
        UnSubscribeOnButtonEvent();
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

        // Setting lock state.
        starsUI.SetActive(true);
        lockedStateUI.SetActive(false);

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
}
