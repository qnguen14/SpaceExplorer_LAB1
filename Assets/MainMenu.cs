using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        // Load the game scene (assuming it's named "GameScene")
        SceneManager.LoadSceneAsync(1);
    }
}
