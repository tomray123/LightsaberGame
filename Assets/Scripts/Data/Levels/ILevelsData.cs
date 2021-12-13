using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelsData
{
    // Returns last level number where player finished his game.
    int GetLastLevelNumber();
    // Returns stars rate by level number.
    int GetStarsRate(int levelNum);
    // Returns level record score by level number.
    int GetLevelRecord(int levelNum);
    // Sets last level number where player finished his game.
    void SetLastLevelNumber(int lastLevelNum);
    // Sets stars rate by level number.
    void SetStarsRate(int levelNum, int rate);
    // Sets level record score by level number.
    void SetLevelRecord(int levelNum, int newRecord);
}
