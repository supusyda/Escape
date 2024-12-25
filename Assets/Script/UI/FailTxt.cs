using System;
using TMPro;
using UnityEngine;

public class FailTxt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform failText;
    void OnEnable()
    {
        GameManager.OnFailRound.AddListener(ShowText);
        GameManager.OnResetScene.AddListener(HideText);
    }
    void OnDisable()
    {
        GameManager.OnFailRound.RemoveListener(ShowText);
        GameManager.OnResetScene.RemoveListener(HideText);
    }

    private void ShowText()
    {
        failText.gameObject.SetActive(true);
    }
    private void HideText()
    {
        failText.gameObject.SetActive(false);
    }
}
