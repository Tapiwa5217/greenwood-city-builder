using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// <summary>
// splash screen.cs
// 06/14/2024
// </summary>

[RequireComponent(typeof(AudioSource))] //Add audio source when attaching the script
public class SplashScreen : MonoBehaviour
{
    public Texture2D _splashScreenBackground; //Creates slot in inspector to assign splash screen background image
    public Texture2D _splashScreenText;       //Creates slot in inspector to assign splash screen text

    private AudioSource _splashScreenAudio;   //Defines naming convention for audio source component
    public AudioClip _splashScreenMusic;      //Creates slot in inspector to assign splash screen music
    
    private float _splashScreenFadeValue;      //Defines fade value
    private float _splashScreenFadeSpeed = 0.3f; //Defines fade speed

    private SplashScreenController _splashScreenController; //Defines naming convention for flash screen

    private enum SplashScreenController {       //Defines states for splash screen
        SplashScreenFadeIn = 0,
        SplashScreenFadeOut = 1
    }

    void Awake() {
        _splashScreenFadeValue = 0;           //Fade value euals zero on start up
    }

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;                       //Set the cursor visible state to false
        Cursor.lockState = CursorLockMode.Locked;     //and lock the cursor

        _splashScreenAudio = GetComponent<AudioSource> ();  //Splash screen audio equals the audio source 
        _splashScreenAudio.volume = 0;                 //Audio volume equals zero on start up
        _splashScreenAudio.clip = _splashScreenMusic;   //Audio clip equals splash screen music 
        _splashScreenAudio.loop = true;                //Set audio to loop
        _splashScreenAudio.Play();                     //Play audio

        _splashScreenController = SplashScreen.SplashScreenController.SplashScreenFadeIn;   //Fade in on start up

        StartCoroutine("SplashScreenManager");    //Start splashScreenManager function
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SplashScreenManager() {
        while(true) {
            switch (_splashScreenController) {
                case SplashScreenController.SplashScreenFadeIn:
                    SplashScreenFadeIn();
                    break;
                case SplashScreenController.SplashScreenFadeOut:
                    SplashScreenFadeOut();
                    break;
                        
            }
            yield return null;
        }
    }

    private void SplashScreenFadeIn() {
        Debug.Log("SplashScreenFadeIn");

        _splashScreenAudio.volume += _splashScreenFadeSpeed * Time.deltaTime; //Increase volume by fade value
        _splashScreenFadeValue += _splashScreenFadeSpeed * Time.deltaTime;    //Increase fade value by fade speed

        if(_splashScreenFadeValue > 1) {     //Fade value is greater than one
            _splashScreenFadeValue = 1;      //Then set the value to one
        }

        if(_splashScreenFadeValue == 1) {    //Fade value equals one
            _splashScreenController = SplashScreen.SplashScreenController.SplashScreenFadeOut;  //Set splash screen controller to equal Splash screen fade out
        }
    }

    private void SplashScreenFadeOut() {
        Debug.Log("SplashScreenFadeOut");

        _splashScreenAudio.volume -= _splashScreenFadeSpeed * Time.deltaTime; //Decrease volume by fade value
        _splashScreenFadeValue -= _splashScreenFadeSpeed * Time.deltaTime;    //Decrease fade value by fade speed

        if(_splashScreenFadeValue < 0) {     //Fade value is less than zero
            _splashScreenFadeValue = 0;      //Then set the value to zero
        }

        if(_splashScreenFadeValue == 0) {    //Fade value equals zero
            SceneManager.LoadScene("Main Menu");     //Loads next scene
        }
    }

    private void OnGUI() {
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), _splashScreenBackground);  //Draw texture starting at 0/0 by the screen width and height thrn draw the background texture 
        
        GUI.color = new Color(1,1,1,_splashScreenFadeValue);  //GUI color is equal to (1,1,1) plus the fade value

        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), _splashScreenText);        //Draw texture starting at 0/0 by the screen width and height thrn draw the splash screen text 
    }
      
}
