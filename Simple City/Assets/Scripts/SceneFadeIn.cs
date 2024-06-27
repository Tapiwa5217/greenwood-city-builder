using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Include the UnityEngine.UI namespace

public class SceneFadeIn : MonoBehaviour
{
    public Image overlayImage; // Reference to the overlay image in the scene

    void Start()
    {
        overlayImage.gameObject.SetActive(true); // Ensure the overlay image is active
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float fadeDuration = 1f; // Duration of the fade-in effect
        float timer = 0f;

        // Fade in effect
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0.5f, 0f, timer / fadeDuration); // Fade in from 50% to 0% alpha
            overlayImage.color = new Color(0, 0, 0, alpha); // Assuming overlay is semi-transparent black
            timer += Time.deltaTime;
            yield return null;
        }

        overlayImage.color = new Color(0, 0, 0, 0f); // Ensure fully faded in
        overlayImage.gameObject.SetActive(false); // Disable the overlay image once fade-in is complete
    }
}
