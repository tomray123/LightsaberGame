using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FactorItem
{
    public int lowerBorder = 0;
    public int higherBorder = 0;
    public int factor = 1;
    public float decreaseVal = 100f;
}

public class ComboScoreController : MonoBehaviour
{
    // Singleton instance.
    public static ComboScoreController instance;

    public int playerHitFineNumber = 300;
    public float playerHitFinePercent = 30;

    [Space]

    // public int timeFineNumber = 100;
    // public float timeFinePercent = 10;

    [Space]

    public List<FactorItem> factorTable = new List<FactorItem>();

    Stack<FactorItem> buffer = new Stack<FactorItem>();
    Stack<FactorItem> factorItems = new Stack<FactorItem>();

    ScoreVisual visual;

    public float ComboScore { get; set; }
    public int ComboFactor { get; private set; }
    public float ComboScorePercent { get; private set; }

    // float decreasePercent = 0;
    // float decreaseVal = 0;

    public void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }

        ComboScore = 0;
        ComboFactor = 1;
        ComboScorePercent = 0;
    }

    private void Start()
    {
        factorTable.Sort((x, y) => x.higherBorder.CompareTo(y.higherBorder));
        for (int i = factorTable.Count - 1; i > 0; i--)
        {
            buffer.Push(factorTable[i]);
        }
        factorItems.Push(factorTable[0]);
        visual = ScoreVisual.instance;

        // Updating visual;
        visual.UpdateCombo(ComboScorePercent, ComboFactor);
    }

    /*
    private void Update()
    {
        if (!PauseController.IsGamePaused)
        {
            DecreaseComboScore();

            // Updating visual;
            visual.UpdateCombo(comboScorePercent, comboFactor);
        }
    }
    */

    public void DecreaseComboScore()
    {
        /* ------------------------- Old one
        decreasePercent = (float)(comboScore * (timeFinePercent / 100));
        
        if (decreasePercent > timeFineNumber)
        {
            decreaseVal = decreasePercent;
        }
        else
        {
            decreaseVal = timeFineNumber;
        }
        decreaseVal = timeFineNumber;
        */

        ComboScore -= factorItems.Peek().decreaseVal * Time.deltaTime;

        if (ComboScore < 0)
            ComboScore = 0;

        CalculateFactor();
    }

    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
    public void onPlayerHit()
    {
        int percent = (int)(ComboScore * (playerHitFinePercent / 100));

        if (percent > playerHitFineNumber)
        {
            ComboScore -= percent;
        }
        else
        {
            ComboScore -= playerHitFineNumber;
        }

        if (ComboScore < 0)
        {
            ComboScore = 0;
        }

        CalculateFactor();

        // Updating visual;
        visual.UpdateCombo(ComboScorePercent, ComboFactor);
    }

    public void CalculateScorePercent(int min, int max, float currentScore)
    {
        ComboScorePercent = (currentScore - min) / (float)(max - min);
    }

    public void CalculateFactor()
    {
        if (factorItems.Peek().higherBorder < ComboScore)
        {
            if(buffer.Count > 1)
            {
                factorItems.Push(buffer.Pop());
            }
        }
        if (factorItems.Peek().lowerBorder > ComboScore)
        {
            if(factorItems.Count > 1)
            {
                buffer.Push(factorItems.Pop());
            }
        }
        ComboFactor = factorItems.Peek().factor;
 
        CalculateScorePercent(factorItems.Peek().lowerBorder, factorItems.Peek().higherBorder, ComboScore);
    }
}
