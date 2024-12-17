using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Circle : MonoBehaviour, ITransition
{
    // Start is called before the first frame update
    public float fadeDuration = 1f;

    public void TransitionIn()
    {
        FadeIn();
    }
    public void TransitionOut()
    {
        FadeOut();
    }
    private Tween FadeIn()
    {
        transform.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;

        return transform.DOScale(37, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
          {
              //   LevelManager.Instance.LoadNextLevel();
          });
        // 
    }
    private Tween FadeOut()
    {
        Debug.Log("FADE OUT", this);
        return transform.DOScale(0, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() => transform.gameObject.SetActive(false));
    }

    IEnumerator ITransition.TransitionIn()
    {
        yield return FadeIn().WaitForCompletion();
    }

    IEnumerator ITransition.TransitionOut()
    {
        yield return FadeOut().WaitForCompletion();
    }
}
