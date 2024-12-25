using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour, iUIShow
{

    [SerializeField] private RectTransform gameOverPanel; // The main panel of the Game Over UI
    [SerializeField] private RectTransform SoundUI; // The main panel of the Game Over UI

    [SerializeField] private TMP_Text gameOverText; // The "Game Over" text
    // [SerializeField] private Button retryButton; // Retry button
    // [SerializeField] private Button quitButton; // Quit button
    [SerializeField] private List<Button> buttons;



    private void Start()
    {
        // Hide the UI initially
        gameOverPanel.localScale = Vector3.zero;
        // gameOverTexts.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, 0);
        buttons.ForEach((btn) =>
        {
            btn.transform.localScale = Vector3.zero;
        });

    }

    public void ShowGameOverUI()
    {
        Sequence sequence = DOTween.Sequence();
        // Scale in the Game Over Panel
        gameOverPanel.gameObject.SetActive(true);
        sequence.Append(gameOverPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce));


        // Fade in the "Game Over" text
        sequence.Append(gameOverText?.DOFade(1, 0.8f).SetDelay(0.5f));

        // Scale in the buttons with a slight delay
        buttons.ForEach((btn) =>
               {
                   //    Debug.Log("FUCK");
                   sequence.Append(btn.transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutBack));
               });

    }

    public void HideGameOverUI()
    {
        // Scale out the panel
        Sequence sequence = DOTween.Sequence();

        gameOverPanel.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);

        // Fade out the text
        gameOverText?.DOFade(0, 0.5f);

        buttons.ForEach((btn) =>
                       {
                           sequence.Append(btn.transform.DOScale(Vector3.zero, .1f).SetEase(Ease.OutBack));
                       });
        sequence.OnComplete(() =>
        {
            gameOverPanel.gameObject.SetActive(false);
            HideSoundUI();


        });

    }
    public void ShowUI()
    {
        // gameObject.SetActive(true);
        ShowGameOverUI();
    }

    public void HideUI()
    {
        // gameObject.SetActive(false);
        HideGameOverUI();
    }

    public void ShowSoundUI()
    {
        SoundUI.GetComponent<iUIShow>().ShowUI();
    }
    public void HideSoundUI()
    {
        SoundUI.GetComponent<iUIShow>().HideUI();
    }
}

