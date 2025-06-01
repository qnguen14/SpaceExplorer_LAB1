using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

   void Update()
    {
        
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
}
