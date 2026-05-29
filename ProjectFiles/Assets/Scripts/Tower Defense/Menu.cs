using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text currencyUI;
    [SerializeField] Text medallionsUI;
    [SerializeField] Color purchsedColour;
    [SerializeField] Color notPurchasedColor;
    [SerializeField] Text speedPurchasedText;
    [SerializeField] Text damagePurchasedText;
    [SerializeField] Button speedUpgradeButton;
    [SerializeField] Button damageUpgradeButton;
    [SerializeField] Text towerNameUI;
    [SerializeField] Image towerIconUI;
    [SerializeField] Text upgradeCostText;
    [SerializeField] GameObject upgradeTab;

    public static Menu main;

    Tower towerToUpgrade;
    int upgradeCost = 50;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        upgradeTab.SetActive(false);
    }

    private void Update()
    {
        currencyUI.text = "Coins: " + LevelManager.main.currency.ToString();
        medallionsUI.text = "Upgrade Medallions: " + LevelManager.main.medallions.ToString();
    }

    public void SetUpgradeMenu(string _towerName, Sprite _towerIcon, bool _hasSpeedUpgrade, bool _hasDamageUpgrade, Tower _towerToUpgrade)
    {
        towerToUpgrade = _towerToUpgrade;

        towerNameUI.text = _towerName;
        towerIconUI.sprite = _towerIcon;
        upgradeCostText.text = "Upgrade Cost: " + upgradeCost.ToString();

        UpdateUpgradeButtons(_hasSpeedUpgrade, _hasDamageUpgrade);

        upgradeTab.SetActive(true);
    }

    private void UpdateUpgradeButtons(bool _speed, bool _damage)
    {
        speedPurchasedText.color = _speed ? purchsedColour : notPurchasedColor;
        speedUpgradeButton.interactable = !_speed;

        damagePurchasedText.color = _damage ? purchsedColour : notPurchasedColor;
        damageUpgradeButton.interactable = !_damage;
    }

    public void PurchaseSpeedUpgrade()
    {
        if (towerToUpgrade != null && !towerToUpgrade.hasSpeedUpgrade && LevelManager.main.SpendMedallions(upgradeCost))
        {
            towerToUpgrade.hasSpeedUpgrade = true;
            UpdateUpgradeButtons(towerToUpgrade.hasSpeedUpgrade, towerToUpgrade.hasDamageUpgrade);
        }
    }

    public void PurchaseDamageUpgrade()
    {
        if (towerToUpgrade != null && !towerToUpgrade.hasDamageUpgrade && LevelManager.main.SpendMedallions(upgradeCost))
        {
            towerToUpgrade.hasDamageUpgrade = true;
            UpdateUpgradeButtons(towerToUpgrade.hasSpeedUpgrade, towerToUpgrade.hasDamageUpgrade);
        }
    }
}