using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelButton
{
    void CloseButton();

    void OpenButton();

    void SetButtonRate(int starsNumber);
}
