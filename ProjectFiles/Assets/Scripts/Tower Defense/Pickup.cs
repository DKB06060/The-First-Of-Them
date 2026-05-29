using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void Awake()
    {
        gameObject.transform.parent = null;
    }

    private void OnMouseOver()
    {
        LevelManager.main.medallions += 5;
        Destroy(gameObject);
    }
}