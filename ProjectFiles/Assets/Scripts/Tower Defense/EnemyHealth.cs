using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Configurables")]
    [SerializeField] int hitPoints = 2;
    [SerializeField] int valueOnDeath = 15;
    [SerializeField] GameObject medallion;

    bool isDestroyed = false;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                Instantiate(medallion, transform.position, Quaternion.identity);
            }
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(valueOnDeath);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void TakeLives()
    {
        LevelManager.main.lives -= hitPoints;
    }
}