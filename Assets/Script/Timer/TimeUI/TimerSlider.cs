using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created public Slider timeSlider; // Reference to the Slider component
    // Duration of the slider in seconds
    // private float elapsedTime;
    bool startCount = false;
    [SerializeField] float startTime = 5;
    Slider timeSlider;
    [SerializeField] Logger logger;

    void Awake()
    {
        timeSlider = GetComponent<Slider>();
    }
    void Start()
    {
        // SetTimeSlider(5); // Start fully filled
    }

    // public void UpdateSlider(float elapsedTime)
    // {

    //     if (duration >= 0)
    //     {


    //         timeSlider.value = duration - elapsedTime; // Decrease over time
    //     }
    // }
    public void SetTimeSlider(float timeLimetSecond)
    {
        startTime = timeLimetSecond;
        timeSlider.maxValue = timeLimetSecond;
        timeSlider.value = timeLimetSecond;
    }
    public void BeginSliderCountDown()
    {
        // StartCoroutine(CountDown());
    }

    // private IEnumerator CountDown()
    // {
    //     float totalTime = startTime;
    //     float elapsedTime = 0;

    //     // Ensure the slider's value starts at 0


    //     while (elapsedTime < totalTime)
    //     {
    //         elapsedTime += Time.deltaTime; // Increase elapsed time
    //         timeSlider.value = Mathf.Clamp01(elapsedTime / totalTime); // Update slider value (0 to 1)
    //         yield return null; // Wait for the next frame
    //     }

    //     // Ensure the slider is set to the maximum value
    //     timeSlider.value = 1f;

    //     // Optional: Trigger an event or action here when the timer completes
    //     Debug.Log("Time Slider Complete!");
    // }
    public void UpdateSlider(float timeRemain)
    {
        timeSlider.value = timeRemain;
    }
}
