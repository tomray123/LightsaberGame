using System;
using UnityEngine;

public class GameSettingsController
{
    public bool GetVisualEffects()
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt("VisualEffects", 1));
    }

    public void SetVisualEffects(bool newValue)
    {
        PlayerPrefs.SetInt("VisualEffects", Convert.ToInt32(newValue));
    }

    public string GetControllerType()
    {
        return PlayerPrefs.GetString("ControllerType", "NoJoystick");
    }

    public void SetControllerType(string newValue)
    {
        PlayerPrefs.SetString("ControllerType", newValue);
    }

    public string GetCorrectionType()
    {
        return PlayerPrefs.GetString("CorrectionType", "linear");
    }

    public void SetCorrectionType(string newValue)
    {
        PlayerPrefs.SetString("CorrectionType", newValue);
    }

    public bool GetSmoothSetting()
    {
        return Convert.ToBoolean(PlayerPrefs.GetInt("SmoothSetting", 0));
    }

    public void SetSmoothSetting(bool newValue)
    {
        PlayerPrefs.SetInt("SmoothSetting", Convert.ToInt32(newValue));
    }
}
