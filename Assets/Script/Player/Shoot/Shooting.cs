using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UnityEvent<Vector2, Vector2, string> ShootBullet = new();//bullet dir , position , prefab name

    protected virtual void Shoot()
    {
        // Get a projectile from the object pool
        // Projectile bulletObject = objectPool.Get();
        // // Launch the projectile
        // bulletObject.Launch(m_MuzzlePosition.position, m_MuzzlePosition.rotation);
        // // Set the next time to shoot
        // nextTimeToShoot = Time.fixedTime + m_CooldownWindow;

        // m_GunFired.Invoke();
    }
}
