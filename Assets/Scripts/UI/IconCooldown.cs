using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconCooldown : MonoBehaviour
{
    public bool isFinishedCooldown = true;

    // Sets max value of icon's mask.
    public void SetMaxState(float percent, Slider mask)
    {
        mask.maxValue = percent;
    }

    // Sets min value of icon's mask.
    public void SetMinState(float percent, Slider mask)
    {
        mask.minValue = percent;
    }

    // Smoothly removes the mask from the icon.
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
