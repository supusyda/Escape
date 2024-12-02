using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    [Header("Bullet Properties")]
    public float speed = 10f;         // Speed of the bullet
    public float lifespan = 5f;      // Lifespan before the bullet gets destroyed
    public int damage = 1;           // Damage dealt by the bullet
    public LayerMask hitLayers;      // Layers the bullet can interact with


}
