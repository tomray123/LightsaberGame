using UnityEngine;

public class VisualEffectsInit : MonoBehaviour, IStartInit
{
    private GameSettingsControllerSO gameSettings = default;

    public void OnStartInit()
    {
        gameSettings = Resources.Load<GameSettingsControllerSO>("ScriptableObjects/GameSettingsController");
        gameObject.SetActive(gameSettings.GetVisualEffects());
    }
}
