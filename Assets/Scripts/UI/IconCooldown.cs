using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconCooldown : MonoBehaviour
{
    //public Slider mask;

    public bool isFinishedCooldown = true;

    public void SetMaxState(float percent, Slider mask)
    {
        mask.maxValue = percent;
    }

    public void SetMinState(float percent, Slider mask)
    {
        mask.minValue = percent;
    }

    public IEnumerator StartCooldown(float duration, Slider mask)
    {
        isFinishedCooldown = false;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            mask.value = Mathf.Lerp(mask.maxValue, mask.minValue, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        mask.value = mask.minValue;
        isFinishedCooldown = true;
    }
}
