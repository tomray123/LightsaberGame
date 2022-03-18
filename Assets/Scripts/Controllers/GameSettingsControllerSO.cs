using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Includes differeent parameters types which are used in PlayerPrefs. 
/// Designed to reduce errors related to string parameters in PlayerPrefs.
/// </summary>
public enum PlayerPrefsParametersType
{
    GraphicsSetting,
    ControllerType,
    CorrectionType,
    SmoothSetting,
    GlobalVolumeSetting
}

/// <summary>
/// Includes simple data types. 
/// Designed to serialize dataTypes in Unity.
/// </summary>
public enum DataTypes
{
    Bool,
    String,
    Int,
    Float
}

[Serializable]
public class DataParameterElement
{
    [Tooltip("String tag for PlayerPrefs")]
    public string tag;
    [Tooltip("Parameter type corresponding to the tag")]
    public PlayerPrefsParametersType accordingParameterEnumType;
    [Tooltip("PlayerPrefs data type corresponding to the tag")]
    public DataTypes dataType;
}

[CreateAssetMenu(fileName = "New GameSettingsControllerSO", menuName = "ScriptableObjects/GameSettingsControllerSO")]
public class GameSettingsControllerSO : ScriptableObject
{
    [SerializeField] private List<DataParameterElement> gameSettingsTable;

    /// <summary>
    /// Searches for data of any type by parameterType in PlayerPrefs.
    /// </summary>
    /// <returns>Bool, Float, Int or String data from PlayerPrefs</returns>
    public object GetPlayerPrefsData(PlayerPrefsParametersType parameterType)
    {
        DataParameterElement searchedElement = FindDataElement(parameterType);
        object returningValue = null;
        switch (searchedElement.dataType)
        {
            case DataTypes.Bool:
                returningValue = Convert.ToBoolean(PlayerPrefs.GetInt(searchedElement.tag, 1));
                break;

            case DataTypes.String:
                returningValue = PlayerPrefs.GetString(searchedElement.tag, "NoJoystick");
                break;

            case DataTypes.Int:
                returningValue = PlayerPrefs.GetInt(searchedElement.tag, 1);
                break;

            case DataTypes.Float:
                returningValue = PlayerPrefs.GetFloat(searchedElement.tag, 1f);
                break;
        }

        return returningValue;
    }

    /// <summary>
    /// Sets any data (string, float, int, bool) to PlayerPrefs by parameterType.
    /// </summary>
    public void SetPlayerPrefsData<T>(PlayerPrefsParametersType parameterType, T newValue)
    {
        DataParameterElement searchedElement = FindDataElement(parameterType);
        switch (searchedElement.dataType)
        {
            case DataTypes.Bool:
                PlayerPrefs.SetInt(searchedElement.tag, Convert.ToInt32(newValue));
                break;

            case DataTypes.String:
                PlayerPrefs.SetString(searchedElement.tag, newValue.ToString());
                break;

            case DataTypes.Int:
                PlayerPrefs.SetInt(searchedElement.tag, Convert.ToInt32(newValue));
                break;

            case DataTypes.Float:
                PlayerPrefs.SetFloat(searchedElement.tag, Convert.ToSingle(newValue));
                break;
        }
    }

    /// <summary>
    /// Searches for bool data by parameterType in PlayerPrefs.
    /// </summary>
    /// <returns>Bool data from PlayerPrefs</returns>
    public bool GetBoolPlayerPrefsData(PlayerPrefsParametersType parameterType)
    {
        string tag = FindTag(parameterType);
        if (tag == null)
        {
            Debug.LogError("Incorrect parameterType or tag for GetBoolPlayerPrefsData.");
            return false;
        }
        return Convert.ToBoolean(PlayerPrefs.GetInt(tag));
    }

    /// <summary>
    /// Sets bool data to PlayerPrefs by parameterType.
    /// </summary>
    public void SetBoolPlayerPrefsData(PlayerPrefsParametersType parameterType, bool newValue)
    {
        string tag = FindTag(parameterType);
        if (tag == null)
        {
            Debug.LogError("Incorrect parameterType or tag for SetBoolPlayerPrefsData.");
            return;
        }
        PlayerPrefs.SetInt(tag, Convert.ToInt32(newValue));
    }

    /// <summary>
    /// Searches for string data by parameterType in PlayerPrefs.
    /// </summary>
    /// <returns>String data from PlayerPrefs</returns>
    public string GetStringPlayerPrefsData(PlayerPrefsParametersType parameterType)
    {
        string tag = FindTag(parameterType);
        if (tag == null)
        {
            Debug.LogError("Incorrect parameterType or tag for GetStringPlayerPrefsData.");
            return null;
        }
        return PlayerPrefs.GetString(tag);
    }

    /// <summary>
    /// Sets string data to PlayerPrefs by parameterType.
    /// </summary>
    public void SetStringPlayerPrefsData(PlayerPrefsParametersType parameterType, string newValue)
    {
        string tag = FindTag(parameterType);
        if (tag == null)
        {
            Debug.LogError("Incorrect parameterType or tag for SetStringPlayerPrefsData.");
            return;
        }
        PlayerPrefs.SetString(tag, newValue);
    }

    /// <summary>
    /// Searches for int data by parameterType in PlayerPrefs.
    /// </summary>
    /// <returns>Int data from PlayerPrefs</returns>
    public int GetIntPlayerPrefsData(PlayerPrefsParametersType parameterType)
    {
        string tag = FindTag(parameterType);
        if (tag == null)
        {
            Debug.LogError("Incorrect parameterType or tag for GetIntPlayerPrefsData.");
            return -1;
        }
        return PlayerPrefs.GetInt(tag);
    }

    /// <summary>
    /// Sets int data to PlayerPrefs by parameterType.
    /// </summary>
    public void SetIntPlayerPrefsData(PlayerPrefsParametersType parameterType, int newValue)
    {
        string tag = FindTag(parameterType);
        if (tag == null)
        {
            Debug.LogError("Incorrect parameterType or tag for SetIntPlayerPrefsData.");
            return;
        }
        PlayerPrefs.SetInt(tag, newValue);
    }

    /// <summary>
    /// Searches for float data by parameterType in PlayerPrefs.
    /// </summary>
    /// <returns>Float data from PlayerPrefs</returns>
    public float GetFloatPlayerPrefsData(PlayerPrefsParametersType parameterType)
    {
        string tag = FindTag(parameterType);
        if (tag == null)
        {
            Debug.LogError("Incorrect parameterType or tag for GetFloatPlayerPrefsData.");
            return -1f;
        }
        return PlayerPrefs.GetFloat(tag);
    }

    /// <summary>
    /// Sets float data to PlayerPrefs by parameterType.
    /// </summary>
    public void SetFloatPlayerPrefsData(PlayerPrefsParametersType parameterType, float newValue)
    {
        string tag = FindTag(parameterType);
        if (tag == null)
        {
            Debug.LogError("Incorrect parameterType or tag for SetFloatPlayerPrefsData.");
            return;
        }
        PlayerPrefs.SetFloat(tag, newValue);
    }

    /// <summary>
    /// Searches for element in gameSettingsTable by parameterType.
    /// </summary>
    /// <returns>DataParameterElement or null.</returns>
    private DataParameterElement FindDataElement(PlayerPrefsParametersType parameterType)
    {
        return gameSettingsTable.Find(x => x.accordingParameterEnumType == parameterType);
    }

    /// <summary>
    /// Searches for tag in gameSettingsTable's element by parameterType.
    /// </summary>
    /// <returns>Found tag or null.</returns>
    private string FindTag(PlayerPrefsParametersType parameterType)
    {
        DataParameterElement searchedElem = gameSettingsTable.Find(x => x.accordingParameterEnumType == parameterType);
        if (searchedElem == null)
        {
            Debug.LogError("Can't find tag with dataType " + parameterType + ". Please check GameSettingsController ScriptableObject.");
            return null;
        }
        else
        {
            return searchedElem.tag;
        }
    }

    public bool GetVisualEffects()
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        return Convert.ToBoolean(PlayerPrefs.GetInt(tag, 1));
    }

    public void SetVisualEffects(bool newValue)
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        PlayerPrefs.SetInt(tag, Convert.ToInt32(newValue));
    }

    public string GetControllerType()
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        return PlayerPrefs.GetString(tag, "NoJoystick");
    }

    public void SetControllerType(string newValue)
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        PlayerPrefs.SetString(tag, newValue);
    }

    public string GetCorrectionType()
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        return PlayerPrefs.GetString(tag, "linear");
    }

    public void SetCorrectionType(string newValue)
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        PlayerPrefs.SetString(tag, newValue);
    }

    public bool GetSmoothSetting()
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        return Convert.ToBoolean(PlayerPrefs.GetInt(tag, 0));
    }

    public void SetSmoothSetting(bool newValue)
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        PlayerPrefs.SetInt(tag, Convert.ToInt32(newValue));
    }

    public bool GetGlobalVolumeSetting()
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        return Convert.ToBoolean(PlayerPrefs.GetInt(tag, 0));
    }

    public void SetGlobalVolumeSetting(bool newValue)
    {
        string tag = FindTag(PlayerPrefsParametersType.GraphicsSetting);
        PlayerPrefs.SetInt(tag, Convert.ToInt32(newValue));
    }
}
