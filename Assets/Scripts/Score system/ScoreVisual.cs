using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVisual : MonoBehaviour
{
    // Singleton instance.
    public static ScoreVisual instance;

    public void Awake()
    {
        // Creating singleton instance.
        if (instance == null)
        {
            instance = this;
        }
    }

    // Simple score counter.
    public Text scoreText;

    // Combo score indicator.
    public Image comboIndi;

    // Combo factor text.
    public Text comboFactorText;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateCombo(float percent, int factor)
    {
        comboIndi.fillAmount = percent;
        comboFactorText.text = factor.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
