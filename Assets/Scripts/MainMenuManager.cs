using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Panel;

    // Reference to the instructions panel (the pop-up)
    public GameObject instructionsPanel;

    void Start()
    {
        Panel = GameObject.Find("Panel");

        // Find the GameObject named "Panel" in the scene and assign it to instructionsPanel:
        instructionsPanel = GameObject.Find("instructionsPanel");

        instructionsPanel.SetActive(false);
        // Alternatively, if you already assigned it in the Inspector, this line is not needed.
    }


    // This method loads the MainScene when the Start button is clicked
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // This method displays the instructions pop-up
    public void ShowInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
            Panel.SetActive(false);

        }
        else
        {
            Debug.LogWarning("Instructions panel not assigned!");
        }
    }

    // This method hides the instructions pop-up
    public void CloseInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
            Panel.SetActive(true);

        }
    }



    // Optionally, you can add a method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
