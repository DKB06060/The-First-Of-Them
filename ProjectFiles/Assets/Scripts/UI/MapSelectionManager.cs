using UnityEngine;
using UnityEngine.UI;

public class MapSelectionManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject mapUI;
    public GameObject confirmPanel;
    public Text confirmText;

    [Header("Scenes")]
    public string location1Scene;
    public string location2Scene;
    public string location3Scene;

    [Header("Buttons")]
    public Button location1Button;
    public Button location2Button;
    public Button location3Button;

    private string selectedScene;
    private AsyncLoader loader;

    void Start()
    {
        loader = FindObjectOfType<AsyncLoader>();
        mapUI.SetActive(false);
        confirmPanel.SetActive(false);
    }

    public void ShowMap()
    {
        mapUI.SetActive(true);
        location1Button.GetComponent<Button>().enabled = true;
        location2Button.GetComponent<Button>().enabled = true;
        location3Button.GetComponent<Button>().enabled = true;
    }

    public void SelectLocation1()
    {
        SelectLocation(location1Scene, "LosAngeles");
    }

    public void SelectLocation2()
    {
        SelectLocation(location2Scene, "SantaBarbara");
    }

    public void SelectLocation3()
    {
        SelectLocation(location3Scene, "Fresno");
    }

    void SelectLocation(string sceneName, string locationName)
    {
        selectedScene = sceneName;

        location1Button.GetComponent<Button>().enabled = false;
        location2Button.GetComponent<Button>().enabled = false;
        location3Button.GetComponent<Button>().enabled = false;

        confirmPanel.SetActive(true);

        confirmText.text = "Commander: Are you sure you want to go to " + locationName + " first?";
    }

    public void ConfirmYes()
    {
        loader.LoadLevelButton(selectedScene);
    }

    public void ConfirmNo()
    {
        confirmPanel.SetActive(false);
        mapUI.SetActive(true);
        location1Button.GetComponent<Button>().enabled = true;
        location2Button.GetComponent<Button>().enabled = true;
        location3Button.GetComponent<Button>().enabled = true;
    }
}