using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitcher : MonoBehaviour
{
    private Button button;
    private Image currentImage;
    private Sprite previousSprite;
    private Sprite nextSprite;
    private bool isOn;
    private GameSettingsControllerSO gameSettings;

    [Tooltip("Sprite to show on enabled button (when state is on)")]
    [SerializeField] private Sprite enabledStateSprite = default;
    [Tooltip("Sprite to show on disabled button (when state is off)")]
    [SerializeField] private Sprite disabledStateSprite = default;

    [Tooltip("Parameter to load/save from PlayerPrefs.")]
    [SerializeField] private PlayerPrefsParametersType dataParameter;

    [Space]
    [Tooltip("Event channel that will handle clicks")]
    [SerializeField] private BoolEventChannelSO eventChannel;

    private void Awake()
    {
        button = GetComponent<Button>();
        currentImage = GetComponent<Image>();
    }

    private void Start()
    {
        // Loading data.
        gameSettings = Resources.Load<GameSettingsControllerSO>("ScriptableObjects/GameSettingsController");
        isOn = gameSettings.GetBoolPlayerPrefsData(dataParameter);

        if (isOn)
        {
            currentImage.sprite = enabledStateSprite;
            previousSprite = currentImage.sprite;
            nextSprite = disabledStateSprite;
        }
        else
        {
            currentImage.sprite = disabledStateSprite;
            previousSprite = currentImage.sprite;
            nextSprite = enabledStateSprite;
        }
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ClickButton);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ClickButton);
    }

    private void ClickButton()
    {
        SwitchSprites();
        isOn = !isOn;
        gameSettings.SetBoolPlayerPrefsData(dataParameter, isOn);
        eventChannel.RaiseEvent(isOn);
    }

    private void SwitchSprites()
    {
        currentImage.sprite = nextSprite;
        nextSprite = previousSprite;
        previousSprite = currentImage.sprite;
    }
}
