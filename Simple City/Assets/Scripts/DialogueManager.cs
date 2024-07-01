using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Explicitly use the UnityEngine.UI namespace
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox; // Reference to the dialogue box panel
    public UnityEngine.UI.Text dialogueText; // Use the fully qualified name for Text
    public Button continueButton; // Reference to the Continue button
    public UnityEngine.UI.Image shadowOverlay;

    private string[] messages = {
        "Welcome to the game! Build your city and manage resources wisely.",
        "Use the currency to build schools, banks, factories, and more.",
        "Balance education, employment, and economic growth to thrive."
    };

    private int currentMessageIndex = 0;

    void Start()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box initially
        shadowOverlay.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false); // Hide the Continue button initially

        continueButton.onClick.AddListener(ShowNextMessage);
        shadowOverlay.color = new Color(0, 0, 0, 0.5f);
        Invoke("StartConversation", 0.5f); // Invoke StartConversation after 5 seconds
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
        shadowOverlay.gameObject.SetActive(true);
        dialogueBox.SetActive(true); // Show the dialogue box
        continueButton.gameObject.SetActive(true); // Show the Continue button
        ShowMessage();
    }

    void ShowMessage()
    {
        if (currentMessageIndex < messages.Length)
        {
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
        dialogueBox.SetActive(false); // Hide the dialogue box
        shadowOverlay.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false); // Hide the Continue button
        // Load the next scene
        SceneManager.LoadScene("TownScene"); // Replace with your actual scene name
    }
}