using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public static LevelManager main;

    [SerializeField] public Transform startPoint;
    [SerializeField] public Transform[] path;

    [SerializeField] public int currency;
    [SerializeField] public int medallions;
    [SerializeField] public int lives = 100;

    [SerializeField] Text livesUI;

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] string sceneToLoad;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
        medallions = 50;

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void Update()
    {
        livesUI.text = "Lives: " +lives.ToString();
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not Enough To Purchse Item");
            return false;
        }
    }

    public void IncreaseMedallions(int amount)
    {
        medallions += amount;
    }

    public bool SpendMedallions(int amount)
    {
        if (amount <= medallions)
        {
            medallions -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not Enough To Upgrade Tower");
            return false;
        }
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
    }

    public void LoseGame()
    {
        loseScreen.SetActive(true);
    }

    public void ResetGame()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        EnemySpawner.main.currentWave = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}