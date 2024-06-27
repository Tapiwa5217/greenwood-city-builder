using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// <summary>
// ChooseCharacter.cs
// Tapiwa Musinga
// 06/18/2024
// </summary>
[RequireComponent(typeof(AudioSource))]         // Add audio source when attaching script
public class ChooseCharacter : ChooseCharacterManager    // Inherit from MonoBehaviour
{
    public Texture2D _selectCharacterTextBackground;        // Create slot in inspector to assign select character text background
    public Texture2D _selectCharacterTextForeground;        // Create slot in inspector to assign select character text foreground
    public Texture2D _selectCharacterText;        // Create slot in inspector to assign select character text
    public Texture2D _selectCharacterArrowLeft;        // Create slot in inspector to assign select character arrow left
    public Texture2D _selectCharacterArrowRight;        // Create slot in inspector to assign select character arrow right

    private float _foregroundTextWidth;                 // Create naming convention for foreground text width
    private float _foregroundTextHeight;                 // Create naming convention for foreground text height
    private float _arrowSize;                           // Create naming convention for arrow size

    public float _chooseCharacterInputTime;         // Defines choose character input timer
    public float _chooseCharacterInputDelay = 1f;   // Defines choose character input delay

    public AudioClip _cycleCharacterButtonPress;    // Creates slot in inspector to assign audio clip when cycling through the characters
    public AudioClip _backgroundMusic;              // Creates slot in inspector to assign background music

    private AudioSource _audioSource;               // Define audio source component
    private GameObject _characterDemo;              // Defines naming convention for selected character game object
    private float _fadeDuration = 1f;               // Duration for fading in/out the music

    public int _characterSelectState;               // Defines naming convention for selected character state

    private enum CharacterSelectionModels           // Defines which character to load
    {
        FemaleDummy = 0,
        MaleDummy = 1,
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();         // Get audio source component
        _audioSource.loop = true;                           // Enable looping for background music
        _audioSource.clip = _backgroundMusic;               // Assign the background music clip
        _audioSource.Play();                                // Play background music
        StartCoroutine(FadeInAudio(_audioSource, _fadeDuration));  // Start fading in the music

        CharacterSelectManager();           // Call CharacterSelectManager on start up

        _foregroundTextWidth = Screen.width / 1.5f;       // Foreground text width equals 1.5f of the screen width on start up
        _foregroundTextHeight = Screen.height / 10f;      // Foreground text height equals 10f of the screen height on start up
        _arrowSize = Screen.height / 10f;                 // Arrow size equals height divided by 10 on start up 
    }

    // Update is called once per frame
    void Update()
    {
        if (_chooseCharacterInputTime > 0)   // If choose character input timer is greater than zero
        {
            _chooseCharacterInputTime -= 1f * Time.deltaTime;  // Then reduce choose character input timer value
        }

        if (_chooseCharacterInputTime > 0)   // If choose character input timer is greater than zero
        {
            return;  // Then do nothing and return
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))  // Check for left arrow key press
        {
            if (_characterSelectState == 0)  // If character select state equals 0
            {
                return;  // Then do nothing and return
            }

            GetComponent<AudioSource>().PlayOneShot(_cycleCharacterButtonPress);  // Get audio component and play cycle button press audio clip

            _characterSelectState--;  // Decrease character select state value
            CharacterSelectManager();  // Call CharacterSelectManager function

            _chooseCharacterInputTime = _chooseCharacterInputDelay;  // Set choose character input timer to input delay
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))  // Check for right arrow key press
        {
            if (_characterSelectState == 1)  // If character select state equals 1
            {
                return;  // Then do nothing and return
            }

            GetComponent<AudioSource>().PlayOneShot(_cycleCharacterButtonPress);  // Get audio component and play cycle button press audio clip

            _characterSelectState++;  // Increase character select state value
            CharacterSelectManager();  // Call CharacterSelectManager function

            _chooseCharacterInputTime = _chooseCharacterInputDelay;  // Set choose character input timer to input delay
        }

        if (Input.GetKeyDown(KeyCode.Return))  // Check for Enter key press
        {
            StartCoroutine(TransitionToNextScene("NextSceneName"));  // Start transitioning to the next scene
        }
    }

    private void CharacterSelectManager()
    {
        switch (_characterSelectState)
        {
            default:
            case (int)CharacterSelectionModels.FemaleDummy:
                FemaleDummy();
                break;
            case (int)CharacterSelectionModels.MaleDummy:
                MaleDummy();
                break;
        }
    }

    private void FemaleDummy()
    {
        Debug.Log("FemaleDummy");

        Destroy(_characterDemo);  // Destroy current character demo object
        _characterDemo = Instantiate(Resources.Load("FemaleDummy")) as GameObject;  // Load and instantiate FemaleDummy from Resources

        _characterDemo.transform.position = new Vector3(-0.5f, 0, -7);  // Set character demo position
    }

    private void MaleDummy()
    {
        Debug.Log("MaleDummy");

        Destroy(_characterDemo);  // Destroy current character demo object
        _characterDemo = Instantiate(Resources.Load("MaleDummy")) as GameObject;  // Load and instantiate MaleDummy from Resources

        _characterDemo.transform.position = new Vector3(-0.5f, 0, -7);  // Set character demo position
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height / 10), _selectCharacterTextBackground); // Draw GUI texture at this position by these dimensions and then draw this texture
        GUI.DrawTexture(new Rect(Screen.width / 2 - (_foregroundTextWidth / 2), 0, _foregroundTextWidth, _foregroundTextHeight), _selectCharacterTextForeground); // Draw GUI texture at this position by these dimensions and then draw this texture
        GUI.DrawTexture(new Rect(Screen.width / 2 - (_foregroundTextWidth / 2), 0, _foregroundTextWidth, _foregroundTextHeight), _selectCharacterText); // Draw GUI texture at this position by these dimensions and then draw this texture
        GUI.DrawTexture(new Rect(Screen.width / 2 - (_foregroundTextWidth / 2) - _arrowSize, 0, _arrowSize, _arrowSize), _selectCharacterArrowLeft); // Draw GUI texture at this position by these dimensions and then draw this texture
        GUI.DrawTexture(new Rect(Screen.width / 2 + (_foregroundTextWidth / 2), 0, _arrowSize, _arrowSize), _selectCharacterArrowRight); // Draw GUI texture at this position by these dimensions and then draw this texture
    }

    // Function to fade in the audio
    IEnumerator FadeInAudio(AudioSource audioSource, float duration)
    {
        float startVolume = 0f;
        audioSource.volume = startVolume;

        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 1f;
    }

    // Function to fade out the audio
    IEnumerator FadeOutAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    // Function to transition to the next scene
    IEnumerator TransitionToNextScene(string sceneName)
    {
        yield return StartCoroutine(FadeOutAudio(_audioSource, _fadeDuration));  // Fade out the audio
        SceneManager.LoadScene(sceneName);  // Load the next scene
    }
}
