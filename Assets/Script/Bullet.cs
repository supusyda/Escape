using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    private float lifeTimer;         // Internal timer to track lifespan
    protected virtual void Start()
    {
        lifeTimer = bulletData.lifespan;
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
        // Get the first contact


        // Slight offset to move it outside of the surface
        Debug.Log("ASS");

        var monoBehaviours = other.transform.Find("Health").GetComponents<MonoBehaviour>();
        foreach (var monoBehaviour in monoBehaviours)
        {
            HandleDamageableInterface(monoBehaviour);
            // HandleEffectTriggerInterface(monoBehaviour, offsetPosition);
        }
    }
    private void HandleDamageableInterface(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour is IDamageAble damageable)
        {
            damageable.TakeDamage(bulletData.damage);
        }
    }
    // Applies damage to the hit object
    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    // Destroys the bullet
    protected virtual void DespawnBullet()
    {
        // BulletSpawner.instance.DespawnOjb(transform);

    }
}

