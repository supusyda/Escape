using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    [SerializeField] private Vector2 moveDirection = Vector2.zero;

    private float lifeTimer;         // Internal timer to track lifespan

    private void OnEnable()
    {
        lifeTimer = bulletData.lifespan;
        GameManager.OnResetScene.AddListener(DespawnBullet);
    }
    private void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(DespawnBullet);
    }
    protected virtual void Update()
    {
        MoveBullet();
        HandleLifespan();
    }

    // Handles bullet movement
    protected virtual void MoveBullet()
    {
        transform.Translate(moveDirection * bulletData.speed * Time.deltaTime);
    }

    // Decreases the bullet's lifespan and destroys it if time is up
    protected void HandleLifespan()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            DespawnBullet();
        }
    }

    // Handles collisions

    private void OnTriggerEnter2D(Collider2D other)
    {

        CheckCollisionInterfaces(other);

    }
    private void CheckCollisionInterfaces(Collider2D other)
    {

        var monoBehaviours = other.transform.Find("Health")?.GetComponents<MonoBehaviour>();
        if (monoBehaviours == null) return;
        foreach (var monoBehaviour in monoBehaviours)
        {
            // HandleEffectTriggerInterface(monoBehaviour, offsetPosition);
            HandleDamageableInterface(monoBehaviour, other.tag);
        }

    }
    private void HandleDamageableInterface(MonoBehaviour monoBehaviour, string hitTag)
    {

        if (hitTag == transform.tag) return;

        if (monoBehaviour is IDamageAble damageable)
        {

            damageable.TakeDamage(bulletData.damage);
        }
        DespawnBullet();
    }
    // Applies damage to the hit object
    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    // Destroys the bullet
    protected virtual void DespawnBullet()
    {
        BulletSpawner.instance.DespawnOjb(transform);

    }
}

