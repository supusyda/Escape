using System;
using UnityEngine;
using UnityEngine.Events;

public class BulletSpawner : Spawn
{
    public static BulletSpawner instance { get; private set; }
    void OnEnable()
    {
        Shooting.ShootBullet.AddListener(ShootBullet);
    }

    private void ShootBullet(Vector2 bulletDir, Vector2 SpawnPos, string bulletName)
    {
        Transform spawnObj = base.SpawnThing(SpawnPos, Quaternion.identity, bulletName);
        spawnObj.GetComponent<BulletBase>().SetMoveDirection(bulletDir);
        spawnObj.gameObject.SetActive(true);
    }


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

}
