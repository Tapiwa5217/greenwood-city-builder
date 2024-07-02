using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// <summary>
// MainMenu.cs
// Tapiwa Musinga
// 06/15/2024
// </summary>
[RequireComponent(typeof(AudioSource))]    // Add audio source when attaching script

public class MainMenu : MonoBehaviour
{
    public int _selectedButton = 0;        // Defines selected GUI button
    public float _timeBetweenButtonPress = 0.1f;   // Defines delay time between button presses
    public float _timeDelay;      // Defines delay variable value

    public float _mainMenuVerticleInputTimer;    // Defines vertical input timer
    public float _mainMenuVerticleInputDelay = 1f;

    public Texture2D _mainMenuBackground;    // Creates slot in inspector to assign main menu background
    public Texture2D _mainMenuTitle;    // Creates slot in inspector to assign main menu title

    private AudioSource _mainMenuAudio;      // Defines naming convention for the main menu audio component
    public AudioClip _mainMenuMusic;         // Creates slot in inspector to assign main menu music
    public AudioClip _mainMenuStartButtonAudio;  // Creates slot in inspector to assign main menu start button audio
    public AudioClip _mainMenuQuitButtonAudio;  // Creates slot in inspector to assign main menu quit button audio

    private float _mainMenuFadeValue;    // Defines fade value
    private float _mainMenuFadeSpeed = 1.5f;  /// Defines fade speed

    public float _mainMenuButtonWidth = 100f;    // Defines main menu button width size
    public float _mainMenuButtonHeight = 25f;    // Defines main menu button height size
    public float _mainMenuGUIOffset = 10f;       // Defines main menu GUI offset

    public bool _startingOnePlayerGame;          // Defines if we are starting a one player game
    public bool _startingTwoPlayerGame;          // Defines if we are starting a two player game
    public bool _quittingGame;                   // Defines if we are quitting the game

    private string[] _mainMenuButtons = new string[] {    // Creates an array of GUI buttons for main menu scene
        "_onePlayer",
        "_twoPlayer",
        "_quit"
    };

    private MainMenuController _mainMenuController;  // Defines naming convention for main menu controller

    private enum MainMenuController {       // Defines states main menu can exist in
        MainMenuFadeIn = 0,
        MainMenuAtIdle = 1,
        MainMenuFadeOut = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        _startingOnePlayerGame = false;     // Starting one player game is false on start up
        _startingTwoPlayerGame = false;     // Starting two player game is false on start up
        _quittingGame = false;              // Quitting game is false on start up

        _mainMenuFadeValue = 0;             // Fade value equals zero on start up

        _mainMenuAudio = GetComponent<AudioSource>();   // mainMenuAudio equals the audio source component

        _mainMenuAudio.volume = 0;     // Audio volume equals zero at start up
        _mainMenuAudio.clip = _mainMenuMusic;    // Audio clip equals main menu music
        _mainMenuAudio.loop = true;      // Set audio to loop
        _mainMenuAudio.Play();       // Play Audio

        _mainMenuController = MainMenu.MainMenuController.MainMenuFadeIn;    // Fade in on start up
        StartCoroutine("MainMenuManager");    // Start MainMenuManager on start up 
    }

    // Update is called once per frame
    void Update()
    {
        // Check keyboard inputs for up and down arrows
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_selectedButton > 0)
            {
                _selectedButton--;
            }
            else
            {
                _selectedButton = _mainMenuButtons.Length - 1; // Wrap around to last button
            }
            _mainMenuVerticleInputTimer = _mainMenuVerticleInputDelay; // Reset vertical input timer
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_selectedButton < _mainMenuButtons.Length - 1)
            {
                _selectedButton++;
            }
            else
            {
                _selectedButton = 0; // Wrap around to first button
            }
            _mainMenuVerticleInputTimer = _mainMenuVerticleInputDelay; // Reset vertical input timer
        }

        // Check if Enter is pressed to select the button
        if (Input.GetKeyDown(KeyCode.Return))
        {
            MainMenuButtonPress();
        }
    }

    private IEnumerator MainMenuManager()
    {
        while (true)
        {
            switch (_mainMenuController)
            {
                case MainMenuController.MainMenuFadeIn:
                    MainMenuFadeIn();
                    break;
                case MainMenuController.MainMenuAtIdle:
                    MainMenuAtIdle();
                    break;
                case MainMenuController.MainMenuFadeOut:
                    MainMenuFadeOut();
                    break;
            }
            yield return null;
        }
    }

    private void MainMenuFadeIn()
    {
       // Debug.Log("MainMenuFadeIn");

        _mainMenuAudio.volume += _mainMenuFadeSpeed * Time.deltaTime;   // Increase the volume of the fade speed

        _mainMenuFadeValue += _mainMenuFadeSpeed * Time.deltaTime;      // Increase fade value by the fade speed

        if (_mainMenuFadeValue > 1)
        {
            _mainMenuFadeValue = 1;                 // then make fade value equals to 1
        }

        if (_mainMenuFadeValue == 1)
        {
            _mainMenuController = MainMenu.MainMenuController.MainMenuAtIdle;   // then make state equal to main menu at idle
        }
    }

    private void MainMenuAtIdle()
    {
        //Debug.Log("MainMenuAtIdle");

        if (_startingOnePlayerGame || _quittingGame == true)
        {
            _mainMenuController = MainMenu.MainMenuController.MainMenuFadeOut;  // Then make state equal to main menu fade out
        }
    }

    private void MainMenuFadeOut()
    {
        Debug.Log("MainMenuFadeOut");

        _mainMenuAudio.volume -= _mainMenuFadeSpeed * Time.deltaTime;   // Decrease the volume of the fade speed

        _mainMenuFadeValue -= _mainMenuFadeSpeed * Time.deltaTime;      // Decrease fade value by the fade speed

        if (_mainMenuFadeValue < 0)
        {
            _mainMenuFadeValue = 0;                 // then make fade value equals to 0
        }

        if (_mainMenuFadeValue == 0 && _startingOnePlayerGame == true)
        {
            SceneManager.LoadScene("SampleScene");   // Load character selection scene
        }
    }

    private void MainMenuButtonPress()
    {
      //  Debug.Log("MainMenuButtonPress");

        GUI.FocusControl(_mainMenuButtons[_selectedButton]);

        switch (_selectedButton)
        {
            case 0:
                _mainMenuAudio.PlayOneShot(_mainMenuStartButtonAudio);   // Play start button audio clip
                _startingOnePlayerGame = true;                           // Set starting one player game to true
                break;
            case 1:
                _mainMenuAudio.PlayOneShot(_mainMenuStartButtonAudio);   // Play start button audio clip
                _startingTwoPlayerGame = true;                           // Set starting two player game to true
                break;
            case 2:
                _mainMenuAudio.PlayOneShot(_mainMenuQuitButtonAudio);   // Play quit button audio clip
                _quittingGame = true;                           // Set quitting game to true
                break;
        }
    }

    void OnGUI()
    {
        if (Time.deltaTime >= _timeDelay && (Input.GetButton("Fire1")))
        {
            MainMenuButtonPress();                              // then start MainMenuButtonPress function
            _timeDelay = Time.deltaTime + _timeBetweenButtonPress;              // and then make time delay equal to current time plus timeBetweenButtonPress
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _mainMenuBackground);  // Draw texture at this position by these dimensions and drawing this texture

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _mainMenuTitle);  // Draw texture at this position by these dimensions and drawing this texture

        GUI.color = new Color(1, 1, 1, _mainMenuFadeValue);           // GUI color is equal to rgb(1,1,1) plus the fade value (alpha)

        GUI.BeginGroup(new Rect(Screen.width / 2 - _mainMenuButtonWidth / 2, Screen.height / 1.5f, _mainMenuButtonWidth, _mainMenuButtonHeight * 3 + _mainMenuGUIOffset * 2)); // Begin GUI Group at this position X and at this position Y by this dimension X by this dimension Y

        // Draw buttons with visual feedback for the selected button
        GUI.SetNextControlName("_onePlayer");       // Set name to one player
        if (GUI.Button(new Rect(0, 0, _mainMenuButtonWidth, _mainMenuButtonHeight), "Play"))
        {
            _selectedButton = 0;            // Set selected button to 0
            MainMenuButtonPress();          // Call MainMenuButtonPress function
        }
        if (_selectedButton == 0) DrawHighlight(new Rect(0, 0, _mainMenuButtonWidth, _mainMenuButtonHeight)); // Highlight selected button

        GUI.SetNextControlName("_twoPlayer");       // Set name to two player
        if (GUI.Button(new Rect(0, _mainMenuButtonHeight + _mainMenuGUIOffset, _mainMenuButtonWidth, _mainMenuButtonHeight), "Settings"))
        {
            _selectedButton = 1;            // Set selected button to 1
            MainMenuButtonPress();          // Call MainMenuButtonPress function
        }
        if (_selectedButton == 1) DrawHighlight(new Rect(0, _mainMenuButtonHeight + _mainMenuGUIOffset, _mainMenuButtonWidth, _mainMenuButtonHeight)); // Highlight selected button

        GUI.SetNextControlName("_quit");       // Set name to quit
        if (GUI.Button(new Rect(0, (_mainMenuButtonHeight + _mainMenuGUIOffset) * 2, _mainMenuButtonWidth, _mainMenuButtonHeight), "Quit"))
        {
            _selectedButton = 2;            // Set selected button to 2
            MainMenuButtonPress();          // Call MainMenuButtonPress function
        }
        if (_selectedButton == 2) DrawHighlight(new Rect(0, (_mainMenuButtonHeight + _mainMenuGUIOffset) * 2, _mainMenuButtonWidth, _mainMenuButtonHeight)); // Highlight selected button

        GUI.EndGroup();     // End GUI Group
    }

    // Method to draw a rounded highlight with 20% opacity
    void DrawHighlight(Rect position)
    {
        // Create a rounded yellow texture with 20% opacity
        Texture2D roundedHighlight = CreateRoundedTexture((int)position.width, (int)position.height, new Color(1, 1, 0, 0.5f)); // Yellow with 50% opacity
        // Draw the rounded highlight texture without changing GUI.color
        GUI.DrawTexture(position, roundedHighlight);
    }

    // Method to create a rounded texture with transparency
    Texture2D CreateRoundedTexture(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(width, height);
        Color transparent = new Color(0, 0, 0, 0);
        float radius = Mathf.Min(width, height) / 5.0f; // Adjust this value for more or less rounding
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Check if the pixel is within the rounded corners
                if (x < radius && y < radius && Vector2.Distance(new Vector2(x, y), new Vector2(radius, radius)) > radius ||
                    x < radius && y > height - radius && Vector2.Distance(new Vector2(x, y), new Vector2(radius, height - radius)) > radius ||
                    x > width - radius && y < radius && Vector2.Distance(new Vector2(x, y), new Vector2(width - radius, radius)) > radius ||
                    x > width - radius && y > height - radius && Vector2.Distance(new Vector2(x, y), new Vector2(width - radius, height - radius)) > radius)
                {
                    texture.SetPixel(x, y, transparent);
                }
                else
                {
                    texture.SetPixel(x, y, color);
                }
            }
        }
        texture.Apply();
        return texture;
    }
}
