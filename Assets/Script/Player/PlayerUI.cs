using System;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject controlPlayerIndicator;





    public void ShowControlPlayerIndicator()
    {
        controlPlayerIndicator.SetActive(true);
    }
    public void HideControlPlayerIndicator()
    {
        controlPlayerIndicator.SetActive(false);
    }
}
