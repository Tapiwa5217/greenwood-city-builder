using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{
    public class AudioPlayer : MonoBehaviour
    {
        // Singleton instance
        public static AudioPlayer instance;

        // Audio clips
        public AudioClip placementSound;
        public AudioClip backgroundMusic;

        // Audio sources
        public AudioSource sfxAudioSource;
        public AudioSource musicAudioSource;

        private void Awake()
        {
            // Ensure singleton pattern
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(this.gameObject);

            // Ensure this object persists across scenes
            DontDestroyOnLoad(this.gameObject);

            // Initialize music audio source
            if (musicAudioSource != null && backgroundMusic != null)
            {
                musicAudioSource.clip = backgroundMusic;
                musicAudioSource.loop = true;
                musicAudioSource.Play();
            }
        }

        public void PlayPlacementSound()
        {
            if (placementSound != null && sfxAudioSource != null)
            {
                sfxAudioSource.PlayOneShot(placementSound);
            }
        }
    }
}
