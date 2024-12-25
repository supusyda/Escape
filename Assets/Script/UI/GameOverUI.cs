using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Transform gameOver;
    void OnEnable()
    {
        // GameManager.OnWinStage.AddListener(ShowGameOver);
        StarterAssetsInputs.OnOpenMenuPress.AddListener(ShowGameOver);
    }

    private void ShowGameOver()
    {
        gameOver.GetComponent<iUIShow>().ShowUI();
    }

    void OnDisable()
    {
        GameManager.OnWinStage.RemoveListener(ShowGameOver);
        StarterAssetsInputs.OnOpenMenuPress.RemoveListener(ShowGameOver);
    }
}
