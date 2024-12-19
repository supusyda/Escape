using System;
using UnityEngine;
using UnityEngine.UI;

public class GameBGMVolumUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void OnEnable()
    {
        Debug.Log("LOAD MIXER");
        slider.value = PlayerPrefs.GetFloat("musicVol", 1);
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        AudioManager.Instance.UpdateMusicVol(value);
    }
}
