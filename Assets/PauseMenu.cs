using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public TextMeshProUGUI pauseText;
    public Button resumeButton;

    private bool isGameOver = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
                ContinueGame();
            else
                PauseGame();
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        // Show the pause panel
        PausePanel.SetActive(true);

        // Set text to "GAME OVER"
        if (pauseText != null)
            pauseText.text = "GAME OVER";

        // Disable the resume button
        if (resumeButton != null)
            resumeButton.interactable = false;

        // Play game over music
        if (MusicManager.instance != null)
            MusicManager.instance.PlayGameOverMusic();

        // Pause the game
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        // Show the pause panel
        PausePanel.SetActive(true);
        // Pause the game
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        /// Reset the game over music state when returning to menu
        if (MusicManager.instance != null)
            MusicManager.instance.ResetGameOverState();

        // Reset game over state
        isGameOver = false;

        // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

        // Resume the game time
        Time.timeScale = 1;
    }

    public void RetryGame()
    {
        // Reset the game over music state when retrying
        if (MusicManager.instance != null)
            MusicManager.instance.ResetGameOverState();

        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        // Reset time scale
        Time.timeScale = 1;
    }


}
