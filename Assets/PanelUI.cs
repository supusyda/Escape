using DG.Tweening;
using UnityEngine;

public class PanelUI : MonoBehaviour, iUIShow
{
    [SerializeField] private RectTransform gamePanel; // The main panel of the Game Over UI

    public void HideUI()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack));
        sequence.OnComplete(() =>
       {
           transform.gameObject.SetActive(false);

       });
    }

    public void ShowUI()
    {
        this.gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        // Scale in the Game Over Panel
        sequence.Append(gamePanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce));



    }
}
