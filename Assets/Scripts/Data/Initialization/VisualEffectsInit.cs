using UnityEngine;

public class VisualEffectsInit : MonoBehaviour, IStartInit
{
    private GameSettingsController gameSettings = new GameSettingsController();

    public void OnStartInit()
    {
        gameObject.SetActive(gameSettings.GetVisualEffects());
    }
}
