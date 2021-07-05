using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    // Singleton instance.
    public static ScoreCounter instance;

    ComboScoreController comboController;

    ScoreVisual visual;

    public int simpleFactorUpperBorder = 5;

    public int totalScore = 0;

    public void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }

        totalScore = 0;
    }

    private void Start()
    {
        comboController = ComboScoreController.instance;
        visual = ScoreVisual.instance;
    }

    public void CountActionScore(int receivedScore, int receivedFactor)
    {
        if (receivedFactor > simpleFactorUpperBorder)
        {
            receivedFactor = simpleFactorUpperBorder;
        }

        totalScore += receivedScore * receivedFactor * comboController.comboFactor;
        comboController.comboScore += receivedScore * receivedFactor;
        comboController.CalculateFactor();

        // Adding visual.
        visual.UpdateScoreText(totalScore);
    }
}
