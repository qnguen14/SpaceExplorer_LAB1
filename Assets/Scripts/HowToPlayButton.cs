using JetBrains.Annotations;
using UnityEngine;

public class HowToPlayButton : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void Open()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("HowToPlayPanel is not assigned in the inspector.");
        }
    }

    public void Close()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("HowToPlayPanel is not assigned in the inspector.");
        }
    }

    public void Toggle()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
        }
        else
        {
            Debug.LogWarning("HowToPlayPanel is not assigned in the inspector.");
        }
    }
}
