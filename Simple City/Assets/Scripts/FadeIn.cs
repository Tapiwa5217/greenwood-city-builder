using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Explicitly use the UnityEngine.UI namespace

public class FadeIn : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // Use the fully qualified name for Image

    void Start()
    {
        StartCoroutine(FadeInEffect());
    }

    IEnumerator FadeInEffect()
    {
        Color fadeColor = fadeImage.color;
        float fadeDuration = 1.5f; // Duration of the fade-in effect

        for (float t = 0.0f; t <= fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadeColor.a = 1.5f - normalizedTime; // Lerp from 1 to 0
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0.0f; // Ensure the alpha is set to 0 after fading
        fadeImage.color = fadeColor;
        fadeImage.gameObject.SetActive(false); // Optionally disable the image after fade-in
    }
}
