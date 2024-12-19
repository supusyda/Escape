using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ParticalSpawner : Spawn
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static ParticalSpawner instance { get; private set; }
    public static UnityEvent<Vector2, Vector2, string> OnSpawnPartical = new();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // Keep across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnSpawnPartical.AddListener(SpawnParticle);
    }

    private void SpawnParticle(Vector2 particleDir, Vector2 SpawnPos, string bulletName)
    {
        Transform spawnObj = base.SpawnThing(SpawnPos, Quaternion.identity, bulletName);
        Vector3 spawnObjFacingDir = new Vector3(Mathf.Abs(spawnObj.localScale.x) * particleDir.x, spawnObj.localScale.y * particleDir.y, 0);
        spawnObj.localScale = spawnObjFacingDir;

        spawnObj.gameObject.SetActive(true);

    }
}
