using System;
using UnityEngine;

public class WaitForInputUI : MonoBehaviour
{
    // Start   is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform waitInputPanel;
    void OnEnable()
    {
        GameManager.OnHasInputActive.AddListener(OnInputActive);
        GameManager.OnResetScene.AddListener(ActivePanel);

    }
    void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(ActivePanel);
        GameManager.OnHasInputActive.RemoveListener(ActivePanel);
    }

    private void ActivePanel()
    {
        waitInputPanel.gameObject.SetActive(true);
    }

    private void OnInputActive()
    {
        waitInputPanel.gameObject.SetActive(false);
    }
}
