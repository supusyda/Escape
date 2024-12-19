using System.Collections;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    public void DespawnObject()
    {
        // Debug.Log("ParticalSpawner.instance" + ParticalSpawner.instance.gameObject.name);
        ParticalSpawner.instance.DespawnOjb(transform);
    }
}
