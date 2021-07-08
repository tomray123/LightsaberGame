using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WinMenuVisual : MonoBehaviour
{
    [SerializeField]
    private float InitStarDelay = 0.5f;
    [SerializeField]
    private float starTimeInterval = 1f;

    [SerializeField]
    private GameObject star1;
    [SerializeField]
    private GameObject star2;
    [SerializeField]
    private GameObject star3;

    [SerializeField]
    private Text currentScore;

    [SerializeField]
    private GameObject oldRecordUI;
    [SerializeField]
    private Text oldRecord;

    [SerializeField]
    private GameObject newRecord;

    private List<GameObject> stars = new List<GameObject>();

    public UnityEvent onStarsLighted;

    // Start is called before the first frame update
    void Start()
    {
        stars.Add(star1);
        stars.Add(star2);
        stars.Add(star3);
    }

    public void ActivateStars(int starCount = 0)
    {
        StartCoroutine(LightStars(starCount));
    }

    private IEnumerator LightStars(int starCount = 0)
    {
        yield return new WaitForSeconds(InitStarDelay);

        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
            yield return new WaitForSeconds(starTimeInterval);
        }

        onStarsLighted.Invoke();
    }

    public void UpdateScoreText(int score)
    {
        currentScore.text = score.ToString();
    }

    public void ShowOldRecord(int score)
    {
        oldRecordUI.SetActive(true);
        oldRecord.text = score.ToString();
    }

    public void EnableNewRecord()
    {
        newRecord.SetActive(true);
    }
}
