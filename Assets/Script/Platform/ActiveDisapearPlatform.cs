using System;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDisapearPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] List<Transform> platforms;
    [SerializeField] ActiveObject activeDisapearPlatform;
    void OnEnable()
    {
        GameManager.OnResetScene.AddListener(RespawnPlatform);
    }
    void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(RespawnPlatform);
    }

    public void DespawnPlatform()
    {
        platforms.ForEach(p => p.gameObject.SetActive(false));
    }
    public void RespawnPlatform()
    {
        platforms.ForEach(p => p.gameObject.SetActive(true));
        activeDisapearPlatform.gameObject.SetActive(true);
    }
}
