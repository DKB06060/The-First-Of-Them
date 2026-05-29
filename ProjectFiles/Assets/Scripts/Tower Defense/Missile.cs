using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("Configurables")]
    [SerializeField] float missileSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] int impactDamage = 10;
    [SerializeField] float explosionRadius = 2.5f;
    [SerializeField] LayerMask enemyLayer;

    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject explosionEffect;

    Transform target;

    public void SetTarget(Transform _target) { target = _target; }

    public void MultiplyDamage(int factor) { impactDamage *= factor; }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Explode();
            return;
        }

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        rb.linearVelocity = direction * missileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float targetAngle = angle - 90f;

        float smoothedAngle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(smoothedAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(impactDamage);
            }
        }

        Destroy(gameObject);
    }
}