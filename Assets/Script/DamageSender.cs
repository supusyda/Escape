using UnityEngine;

public class DamageSender : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] protected int damage = 1;
    void OnTriggerEnter2D(Collider2D other)
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
            HandleDamageableInterface(damageable);
        }
        if (hitParticle != null)
        {
            HandleEffectTriggerInterface(hitParticle, other.transform.position);
        }

    }

    private void HandleEffectTriggerInterface(Target hitParticle, Vector3 contacts)
    {

        hitParticle.Hit(contacts);

    }


    private void HandleDamageableInterface(IDamageAble damageable)
    {

        Debug.Log("DEDUCCE HELATH");


        damageable.TakeDamage(damage);
    }
}
