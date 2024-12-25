using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;// data of the bullet SO
    [SerializeField] private Vector2 moveDirection = Vector2.zero;
    // [SerializeField] ContactFilter2D contactFilter;

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

        // List<ContactPoint2D> contacts = new();
        // transform.GetComponent<Collider2D>().GetContacts(contacts);
        // other.GetContacts(bulletData.hitLayers, contacts);


        Transform contactTransform = other.transform;

        Target hitParticle = contactTransform.GetComponent<Target>();
        IDamageAble damageable = contactTransform.Find("Health")?.GetComponent<IDamageAble>();
        if (other.tag == transform.tag) return;
        // if()
        if (damageable != null)
        {
            HandleDamageableInterface(damageable, other.tag);
        }
        if (hitParticle != null)
        {
            HandleEffectTriggerInterface(hitParticle, transform.position);
        }

    }

    private void HandleEffectTriggerInterface(Target hitParticle, Vector3 contacts)
    {

        hitParticle.Hit(contacts);
        DespawnBullet();
    }
    void DebugContactPoints(List<ContactPoint2D> contacts, int contactCount)
    {
        for (int i = 0; i < contactCount; i++)
        {
            ContactPoint2D contact = contacts[i];

            // Draw the contact point in the Scene view
            Debug.DrawLine(contact.point, contact.point + Vector2.up * 10f, Color.red, 5f);

            Debug.Log($"Contact with {contact.collider.name} at {contact.point}");
        }
    }

    private void HandleDamageableInterface(IDamageAble damageable, string hitTag)
    {




        damageable.TakeDamage(bulletData.damage);


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

