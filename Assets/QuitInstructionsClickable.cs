using UnityEngine;

public class QuitInstructionsClickable : MonoBehaviour
{
    // Reference to your MainMenuManager, which has the CloseInstructions() method
    public MainMenuManager mainMenuManager;

    void OnMouseDown()
    {
        if (mainMenuManager != null)
        {
            mainMenuManager.CloseInstructions();
            Debug.Log("X icon clicked - instructions closed.");
        }
        else
        {
            Debug.LogWarning("MainMenuManager reference is missing!");
        }
    }
}
