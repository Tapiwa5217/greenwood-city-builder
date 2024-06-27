using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager; // Reference to the DialogueManager

    void Start()
    {
        // Ensure the DialogueManager reference is assigned
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartConversation();
    }
}
