using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuController : MonoBehaviour, IAwakeInit
{
    // Name on gameobject which contains level pages.
    [SerializeField]
    private string levelsHolderName = "Levels Holder";

    public List<ILevelButton> ButtonsHolder { get; private set; }
    private Transform levelsHolder;
    private LevelMenuUpdater menuUpdater;

    public void OnAwakeInit()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Initializing list.
        ButtonsHolder = new List<ILevelButton>();
        menuUpdater = new LevelMenuUpdater(ButtonsHolder);

        // Finding levels.
        levelsHolder = transform.Find(levelsHolderName);
        if (!levelsHolder)
        {
            Debug.LogError("Can't find levels holder with name '" + levelsHolderName + "'");
            return;
        }

        // Adding buttons to list.
        AddButtons();
    }

    // Adds level menu buttons to button holder.
    public void AddButtons()
    {
        for (int i = 0; i < levelsHolder.childCount; i++)
        {
            Transform page = levelsHolder.GetChild(i);
            for (int j = 0; j < page.childCount; j++)
            {
                ButtonsHolder.Add(page.GetChild(j).GetComponent<ILevelButton>());
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(menuUpdater.UpdateLevelMenu());
    }
}
