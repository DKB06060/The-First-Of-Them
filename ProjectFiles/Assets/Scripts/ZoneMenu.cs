using UnityEngine;
using UnityEngine.UI;

public class ZoneMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject zone1UI;
    [SerializeField] GameObject zone1;
    [SerializeField] GameObject menuAssets;

    private void Start()
    {
        zone1.SetActive(false);
        zone1UI.SetActive(false);
        menuAssets.SetActive(true);
    }

    public void Zone1()
    {
        Debug.Log("Loading Zone 1");
        zone1.SetActive(true);
        zone1UI.SetActive(true);
        menuAssets.SetActive(false);
    }
}