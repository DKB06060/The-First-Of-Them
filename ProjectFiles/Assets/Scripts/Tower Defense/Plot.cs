using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    public Tower towerScript;
    private Color startColour;

    private void Start()
    {
        startColour = sr.color;

        if (towerObj != null && towerScript == null)
        {
            towerScript = towerObj.GetComponent<Tower>();
        }
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColour;
    }

    private void OnMouseDown()
    {
        if (towerObj != null)
        {
            if (towerScript == null)
            {
                towerScript = towerObj.GetComponent<Tower>();
            }

            if (Menu.main != null && towerScript != null)
            {
                Menu.main.SetUpgradeMenu(
                    towerScript.towerName,
                    towerScript.towerIcon,
                    towerScript.hasSpeedUpgrade,
                    towerScript.hasDamageUpgrade,
                    towerScript
                );
            }
            else if (Menu.main == null)
            {
                Debug.LogError("Menu.main is missing! Make sure the Menu script has 'main = this' in Awake.");
            }

            return;
        }

        TowerComponents towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild == null)
        {
            Debug.Log("No tower selected to build.");
            return;
        }

        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("You Cannot Afford This Tower");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);

        towerScript = towerObj.GetComponent<Tower>();

        if (towerScript == null)
        {
            Debug.LogError("The Tower Prefab is missing the 'Tower' script!");
        }
    }
}