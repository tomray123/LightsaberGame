using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuUpdater
{
    private ILevelsData levelsData;
    private List<ILevelButton> buttonsHolder;

    public LevelMenuUpdater(List<ILevelButton> buttonsHolder)
    {
        this.buttonsHolder = buttonsHolder;
        levelsData = new LevelsDataPlayerPrefs();
    }

    public IEnumerator UpdateLevelMenu()
    {
        int lastLevelNumber = levelsData.GetLastLevelNumber();

        for (int i = 0; i < lastLevelNumber; i++)
        {
            if (i % 15 == 0)
            {
                yield return null;
            }
            buttonsHolder[i].OpenButton();
            buttonsHolder[i].SetButtonRate(levelsData.GetStarsRate(i + 1));
        }

        buttonsHolder[lastLevelNumber].OpenButton();

        for (int i = lastLevelNumber + 1; i < buttonsHolder.Count; i++)
        {
            buttonsHolder[i].CloseButton();
            if (i > lastLevelNumber && i % 15 == 0)
            {
                yield return null;
            }
        }
    }
}
