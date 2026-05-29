using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] TowerComponents[] towers;

    int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public TowerComponents GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        Debug.Log("Selected Turret Index: " + selectedTower);
        selectedTower = _selectedTower;
    }
}