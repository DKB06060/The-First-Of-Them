using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject mainMenuAssets;

    private void Start()
    {
        creditsPanel.SetActive(false);
        mainMenuAssets.SetActive(true);
    }

    public void Credits()
    {
        mainMenuAssets.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        creditsPanel.SetActive(false);
        mainMenuAssets.SetActive(true);
    }
}