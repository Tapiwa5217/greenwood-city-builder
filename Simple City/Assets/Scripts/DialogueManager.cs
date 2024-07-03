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
        "Deep in the heart of a verdant valley, embraced by rolling hills and sparkling rivers, lies a plot of untouched land ripe with potential. From this serene landscape, you envision the foundations of a bustling metropolis, where skyscrapers will one day touch the sky.",
        "As your city flourishes, prioritize the building of schools and banks. Education is the bedrock of progress, nurturing a skilled workforce and attracting innovative businesses eager to contribute to your city's prosperity.",
        "Every street and block you plan defines the character of your city. Strategically balance residential areas for families seeking homes, commercial districts for bustling markets, and industrial zones for factories powering economic growth. Your choices will shape not just a city, but a thriving community.",
        "Managing your city's resources is crucial. Monitor budgets, allocate funds wisely between infrastructure, services, and public amenities. A balanced budget ensures sustainable growth and prosperity.",
        "Navigate challenges such as traffic congestion, environmental pollution, and ensuring citizen happiness. Implement effective transportation systems, green initiatives, and community services to maintain harmony and growth.",
        "Click to continue and start building your dream city today!"
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
