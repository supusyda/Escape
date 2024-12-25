using System;
using DG.Tweening;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public float scaleAmount = 1.1f; // Slightly bigger scale
    public float duration = 0.5f;    // Duration of the scaling effect
    Tween tween;
    [SerializeField] private bool activeOnEnable = false;
    void OnEnable()
    {
        if (activeOnEnable)
        {
            BeginScale();
        }
    }
    void OnDisable()
    {
        if (activeOnEnable)
        {
            EndScale();
        }
    }
    public Tween TweenScale()
    {
        // Original scale
        Vector3 originalScale = transform.localScale;

        // Animate to a slightly larger scale and back
        return transform.DOScale(originalScale * scaleAmount, duration)
             .SetLoops(-1, LoopType.Yoyo) // Infinite loop with a ping-pong effect
             .SetEase(Ease.InOutSine);   // Smooth easing
    }
    public void BeginScale()
    {
        tween = TweenScale();
    }

    public void EndScale()
    {
        transform.localScale = Vector3.one; // Reset the scale
        tween.Kill(); // Stop the tween
    }


}