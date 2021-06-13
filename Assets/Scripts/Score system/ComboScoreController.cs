using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FactorItem
{
    public int lowerBorder = 0;
    public int higherBorder = 0;
    public int factor = 1;
}

public class ComboScoreController : MonoBehaviour
{
    // Singleton instance.
    public static ComboScoreController instance;

    public int playerHitFineNumber = 300;
    public float playerHitFinePercent = 30;

    [Space]

    public float fineTime = 1f;
    public int timeFineNumber = 100;
    public float timeFinePercent = 10;

    [Space]

    public List<FactorItem> factorTable = new List<FactorItem>();

    Stack<FactorItem> buffer = new Stack<FactorItem>();
    Stack<FactorItem> factorItems = new Stack<FactorItem>();

    [HideInInspector]
    public int comboScore = 0;

    [HideInInspector]
    public int comboFactor = 1;

    int decreasePercent = 0;
    int decreaseVal = 0;

    public void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }

        comboScore = 0;
        comboFactor = 1;
    }

    private void Start()
    {
        factorTable.Sort((x, y) => x.higherBorder.CompareTo(y.higherBorder));
        for (int i = factorTable.Count - 1; i > 0; i--)
        {
            buffer.Push(factorTable[i]);
        }
        factorItems.Push(factorTable[0]);
    }

    private void Update()
    {
        if (!PauseController.IsGamePaused)
        {
            DecreaseComboScore();
        }
    }

    public void DecreaseComboScore()
    {
        decreasePercent = (int)(comboScore * (timeFinePercent / 100));

        if (decreasePercent > timeFineNumber)
        {
            decreaseVal = decreasePercent;
        }
        else
        {
            decreaseVal = timeFineNumber;
        }

        if(comboScore > 0)
        {
            comboScore -= (int)(decreaseVal * Time.deltaTime);
        }
        else
        {
            comboScore = 0;
        }
        CalculateFactor();
    }

    public void onPlayerHit()
    {
        int percent = (int)(comboScore * (playerHitFinePercent / 100));

        if (percent > playerHitFineNumber)
        {
            comboScore -= percent;
        }
        else
        {
            comboScore -= playerHitFineNumber;
        }

        if (comboScore < 0)
        {
            comboScore = 0;
        }

        CalculateFactor();
    }

    public void CalculateFactor()
    {
        if (factorItems.Peek().higherBorder < comboScore)
        {
            factorItems.Push(buffer.Pop());
        }
        if (factorItems.Peek().lowerBorder > comboScore)
        {
            buffer.Push(factorItems.Pop());
        }
        comboFactor = factorItems.Peek().factor;
        // Debug.Log("CCCCCCCC" + comboScore);
    }
}
