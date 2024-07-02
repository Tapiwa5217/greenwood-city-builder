using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox; // Reference to the dialogue box panel
    public Text dialogueText; // Reference to the Text component
    public Button continueButton; // Reference to the Continue button
    public Image shadowOverlay; // Reference to the shadow overlay
    public ButtonSoundPlayer buttonSoundPlayer; // Reference to the ButtonSoundPlayer

    private string[] messages = {
        "Welcome to the game! Build your city and manage resources wisely.",
        "Use the currency to build schools, banks, factories, and more.",
        "Balance education, employment, and economic growth to thrive."
    };

    private int currentMessageIndex = 0;

    void Start()
    {
        if (dialogueBox == null || dialogueText == null || continueButton == null || shadowOverlay == null || buttonSoundPlayer == null)
        {
            
            return;
        }

        dialogueBox.SetActive(false); // Hide the dialogue box initially
        shadowOverlay.gameObject.SetActive(false); // Hide the shadow overlay initially
        continueButton.gameObject.SetActive(false); // Hide the Continue button initially

        continueButton.onClick.AddListener(ShowNextMessage);
        continueButton.onClick.AddListener(buttonSoundPlayer.PlayButtonClickSound); // Add sound listener

        shadowOverlay.color = new Color(0, 0, 0, 0.5f);

        Invoke("StartConversation", 0.5f); // Invoke StartConversation after 0.5 seconds
    }

    void Update()
    {
        // Check if the continue button (e.g., space key) is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShowNextMessage();
        }
    }

    public void StartConversation()
    {
        currentMessageIndex = 0; // Reset the message index

        if (shadowOverlay != null) shadowOverlay.gameObject.SetActive(true);
        if (dialogueBox != null) dialogueBox.SetActive(true); // Show the dialogue box
        if (continueButton != null) continueButton.gameObject.SetActive(true); // Show the Continue button

        ShowMessage();
    }

    void ShowMessage()
    {
        if (currentMessageIndex < messages.Length)
        {
            if (dialogueText != null)
                dialogueText.text = messages[currentMessageIndex];
        }
        else
        {
            EndConversation();
        }
    }

    void ShowNextMessage()
    {
        currentMessageIndex++;
        ShowMessage();
    }

    void EndConversation()
    {
        if (dialogueBox != null) dialogueBox.SetActive(false); // Hide the dialogue box
        if (shadowOverlay != null) shadowOverlay.gameObject.SetActive(false); // Hide the shadow overlay
        if (continueButton != null) continueButton.gameObject.SetActive(false); // Hide the Continue button

        // Load the next scene
        SceneManager.LoadScene("TownScene"); // Replace with your actual scene name
    }
}
