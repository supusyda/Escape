using System;
using UnityEngine;

public class WinGamePanel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform gameOver;
    void OnEnable()
    {
        GameManager.OnWinStage.AddListener(ShowGameOver);

    }

    private void ShowGameOver()
    {
        gameOver.GetComponent<iUIShow>().ShowUI();
    }

    void OnDisable()
    {
        GameManager.OnWinStage.RemoveListener(ShowGameOver);

    }
}
