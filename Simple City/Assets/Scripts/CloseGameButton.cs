using UnityEngine;

public class CloseGameButton : MonoBehaviour
{
    public void CloseGame()
    {
        // If we are running in a standalone build of the game
#if UNITY_STANDALONE
            // Quit the application
            Application.Quit();
#endif

        // If we are running in the editor
#if UNITY_EDITOR
            // Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
