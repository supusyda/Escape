using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform holder;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObj;
    // protected virtual void Awake()
    // {
    //     Spawn.instance = this;
    // }
    // void ()
    // {
    //     this.LoadPrefabs();
    //     this.LoadHolder();
    // }
    private void OnEnable()
    {
        // if (poolObj != null) {}
        this.LoadPrefabs();
        this.LoadHolder();
    }
    protected void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.Log(transform.name + ": LoadHolder", gameObject);
    }
    protected Transform GetObjFromPool(Transform prefab)
    {
        foreach (Transform obj in poolObj)
        {
            if (obj.name == prefab.name)
            {
                this.poolObj.Remove(obj);
                return obj;
            }
        }
        Transform newPerfabs = Instantiate(prefab);
        newPerfabs.name = prefab.name;
        return newPerfabs;
    }
    void LoadPrefabs()
    {
        Transform prefabOjb = transform.Find("Prefabs");
        foreach (Transform prefab in prefabOjb)
        {
            prefabs.Add(prefab);
        }
        Hide();
    }
    void Hide()
    {
        foreach (Transform prefab in prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    public Transform SpawnThing(Vector3 spawnPos, Quaternion direction, string prefabName)
    {
        Transform prefab = GetPreFabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefabs not found" + prefabName);
        }
        // Transform newObj = Instantiate(prefab, spawnPos, direction);
        Transform newObj = GetObjFromPool(prefab);
        newObj.SetLocalPositionAndRotation(spawnPos, direction);
        newObj.SetParent(holder);
        // Transform new
        return newObj;
    }
    public void DespawnOjb(Transform ojb)
    {
        this.poolObj.Add(ojb);
        ojb.gameObject.SetActive(false);
    }
    protected Transform GetPreFabByName(string prefabName)
    {
        foreach (Transform prefab in prefabs)

        {
            if (prefab.name == prefabName)
                return prefab;
        }
        return null;
    }
}
