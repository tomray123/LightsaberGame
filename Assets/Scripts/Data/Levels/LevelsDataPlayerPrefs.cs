using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsDataPlayerPrefs : ILevelsData
{
    // Returns last level number where player finished his game.
    public int GetLastLevelNumber()
    {
        return PlayerPrefs.GetInt("PlayerLastLevel");
    }

    // Returns stars rate by level number.
    public int GetStarsRate(int levelNum)
    {
        return PlayerPrefs.GetInt("stars_lvl" + levelNum.ToString());
    }

    // Returns level record score by level number.
    public int GetLevelRecord(int levelNum)
    {
        return PlayerPrefs.GetInt("rec_lvl" + levelNum.ToString());
    }

    // Sets last level number where player finished his game.
    public void SetLastLevelNumber(int lastLevelNum = 0)
    {
        PlayerPrefs.SetInt("PlayerLastLevel", lastLevelNum);
    }

    // Sets stars rate by level number.
    public void SetStarsRate(int levelNum, int rate = 0)
    {
        PlayerPrefs.SetInt("stars_lvl" + levelNum.ToString(), rate);
    }

    // Sets level record score by level number.
    public void SetLevelRecord(int levelNum, int newRecord = 0)
    {
        PlayerPrefs.SetInt("rec_lvl" + levelNum.ToString(), newRecord);
    }
}
