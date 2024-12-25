using System.Collections;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float countdownTime = 10f; // Total countdown time in seconds

    public TMP_Text countdownTMP; // Optional: TextMeshPro component


    bool startCount = false;
    float startTime = 0;


    public void SetTimeCount(float time)
    {
        startTime = time;
        countdownTMP.text = time.ToString("00");

    }
    public void BeginCount()
    {
        // StartCoroutine(Countdown());

    }

    void OnDisable()
    {
        // StopCoroutine(Countdown());
    }

    // private IEnumerator Countdown()
    // {
    //     float timeLeft = startTime;

    //     while (timeLeft > 0)
    //     {
    //         countdownTMP.text = timeLeft.ToString();
    //         yield return new WaitForSeconds(1); // Wait for 1 second
    //         timeLeft--;
    //     }

    //     // countdownTMP.text = "GO!"; // Display final message

    // }
    public void UpdateCountdownDisplay(float currentTime)
    {
        // Format time to "SS"iflog 

        int displayTime = currentTime > 0 ? Mathf.CeilToInt(currentTime) : 0;
        string timeFormatted = Mathf.FloorToInt(displayTime % 60).ToString("00");

        // Update Text or TextMeshPro

        if (countdownTMP != null)
            countdownTMP.text = timeFormatted;
    }

}
