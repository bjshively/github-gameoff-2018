using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenFade : MonoBehaviour
{
    public void DeathScreenFade(Image i)
    {
        StartCoroutine(BeginImageFade(i));
    }

    IEnumerator BeginImageFade(Image i)
    {
        float timestamp = Time.time;
        float waitTime = 3;

        while ((i.color.a < 0.54f) && (timestamp + Time.deltaTime < timestamp + waitTime))
        {
            Color newAlpha = i.color;
            newAlpha.a += Time.deltaTime / waitTime;
            i.color = newAlpha;
            yield return null;
        }

        yield return null;
    }

    public void DeathTextFade(TMP_Text t)
    {
        StartCoroutine(BeginTextFade(t));
    }

    IEnumerator BeginTextFade(TMP_Text t)
    {
        float timestamp = Time.time;
        float waitTime = 3.5f;

        while ((t.color.a < .87) && (timestamp + Time.deltaTime < timestamp + waitTime))
        {
            Color newAlpha = t.color;
            newAlpha.a += Time.deltaTime / waitTime;
            t.color = newAlpha;
            yield return null;
        }

        yield return null;
    }
}