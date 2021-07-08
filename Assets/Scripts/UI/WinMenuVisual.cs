using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<ParticleSystem> ps = new List<ParticleSystem>();

    private List<GameObject> stars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        stars.Add(star1);
        stars.Add(star2);
        stars.Add(star3);
        for (int i = 0; i < stars.Count; i++)
        {
            ps.Add(stars[i].GetComponent<ParticleSystem>());
        }
    }

    public void ActivateStars(int starCount = 0)
    {
        StartCoroutine(LightStars(starCount));
    }

    private IEnumerator LightStars(int starCount = 0)
    {
        yield return new WaitForSecondsRealtime(InitStarDelay);

        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
            ps[i].Play();
            yield return new WaitForSecondsRealtime(starTimeInterval);
        }
    }
}
