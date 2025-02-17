using UnityEngine;

public class QuitInstructionsClickable : MonoBehaviour
{
    // Reference to your MainMenuManager, which has the CloseInstructions() method
    public MainMenuManager mainMenuManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Detect ESC key press
        {
            if (mainMenuManager != null)
            {
                mainMenuManager.CloseInstructions();
                Debug.Log("ESC key pressed - instructions closed.");
            }
            else
            {
                Debug.LogWarning("MainMenuManager reference is missing!");
            }
        }
    }
}
