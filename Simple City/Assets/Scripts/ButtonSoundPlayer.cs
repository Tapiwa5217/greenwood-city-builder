using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    public AudioClip buttonClickSound; // Assign the sound clip in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            // Check if the AudioSource component is enabled
            if (!audioSource.enabled)
            {
                audioSource.enabled = true; // Ensure AudioSource is enabled
            }

            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
