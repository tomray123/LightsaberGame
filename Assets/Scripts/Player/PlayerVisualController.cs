using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisualController : MonoBehaviour
{
    private Animator animator;

    [HideInInspector]
    public Image HitVignette;

    [SerializeField]
    private float vignetteFadeDuration = 1f;
    [SerializeField]
    private float vignetteFadeMaxValue = 0.6f;
    [SerializeField]
    private float alphaHitStep = 0.2f;

    private Coroutine fadeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartDeathAnimation()
    {
        animator.SetBool("isDead", true);
    }

    public void GetHit()
    {
        Color cl = HitVignette.color;
        cl.a += alphaHitStep;
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeVignette(cl.a));
    }

    // Sets the transparency of vignette.
    private IEnumerator FadeVignette(float startValue)
    {
        if(startValue > vignetteFadeMaxValue)
        {
            startValue = vignetteFadeMaxValue;
        }
        float timeElapsed = 0;
        float i;
        Color cl;
        while (timeElapsed < vignetteFadeDuration)
        {
            i = Mathf.Lerp(startValue, 0f, timeElapsed / vignetteFadeDuration);
            timeElapsed += Time.deltaTime;
            cl = HitVignette.color;
            cl.a = i;
            HitVignette.color = cl;
            yield return null;
        }
        i = 0f;
        cl = HitVignette.color;
        cl.a = i;
        HitVignette.color = cl;
        fadeCoroutine = null;
    }
}
