using UnityEngine;

public enum Device
{
    PC,
    Smartphone
}

[CreateAssetMenu(fileName ="New game settings", menuName = "ScriptableObjects/Game Settings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    [SerializeField]
    private Device targetDevice = Device.PC;
    public Device TargetDevice
    {
        get
        {
            return targetDevice;
        }
    }

    [SerializeField]
    private bool graphicsEnabled = true;
    public bool GraphicsEnabled
    {
        get
        {
            return graphicsEnabled;
        }
    }

    [SerializeField]
    private bool isSmooth = false;
    public bool IsSmooth
    {
        get
        {
            return isSmooth;
        }
    }

    [SerializeField]
    private string controllerType = "NoJoystick";
    public string ControllerType
    {
        get
        {
            return controllerType;
        }
    }

    [SerializeField]
    private string correctionType = "linear";
    public string CorrectionType
    {
        get
        {
            return correctionType;
        }
    }

    [SerializeField]
    private bool globalVolumeOn = true;
    public bool GlobalVolumeOn
    {
        get
        {
            return globalVolumeOn;
        }
    }
}
