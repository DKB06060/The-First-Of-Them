using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Sprite basicSprite;
    [SerializeField] private Sprite damageUpgradedSprite;
    [SerializeField] private Sprite speedUpgradedSprite;
    [SerializeField] private Sprite fullyUpgradedSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Configurables")]
    [SerializeField] public string towerName;
    [SerializeField] public Sprite towerIcon;
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] public float bps = 1f;

    private Transform target;
    private float timeUntilFire;

    public bool hasSpeedUpgrade = false;
    public bool hasDamageUpgrade = false;

    private void Update()
    {
        UpdateSprite();

        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject projectileObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Missile missileScript = projectileObj.GetComponent<Missile>();
        if (missileScript != null)
        {
            if (hasDamageUpgrade) missileScript.MultiplyDamage(2);
            missileScript.SetTarget(target);
            return;
        }

        Bullet bulletScript = projectileObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            if (hasDamageUpgrade) bulletScript.bulletDamage *= 2;
            bulletScript.SetTarget(target);
        }
    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
        turretRotationPoint.rotation = Quaternion.Lerp(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void ApplySpeedUpgrade()
    {
        if (hasSpeedUpgrade) return;

        hasSpeedUpgrade = true;
        bps *= 2f;
    }

    public void ApplyDamageUpgrade()
    {
        hasDamageUpgrade = true;
    }

    void UpdateSprite()
    {
        if (!hasDamageUpgrade && !hasSpeedUpgrade)
        {
            spriteRenderer.sprite = basicSprite;
        }

        if (!hasDamageUpgrade && hasSpeedUpgrade)
        {
            spriteRenderer.sprite = speedUpgradedSprite;
        }

        if (hasDamageUpgrade && !hasSpeedUpgrade)
        {
            spriteRenderer.sprite = damageUpgradedSprite;
        }

        if (hasDamageUpgrade && hasSpeedUpgrade)
        {
            spriteRenderer.sprite = fullyUpgradedSprite;
        }
    }
}