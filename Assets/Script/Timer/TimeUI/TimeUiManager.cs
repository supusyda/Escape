using System.Collections;
using UnityEngine;

public class TimeUiManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TimerSlider timerSlider;
    [SerializeField] CountDownUI countDownUI;

    [SerializeField] float RoundTimeLimit;
    private float elapseTime;
    private float currentTimeRemain;
    private Coroutine _coundownCorotine;
    private bool _isCountdown = true;
    void OnEnable()
    {
        GameManager.OnResetScene.AddListener(InitCount);
        GameManager.OnWinStage.AddListener(StopCountDownBySec);
        GameManager.OnHasInputActive.AddListener(BeginCount);
    }
    void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(InitCount);
        GameManager.OnWinStage.RemoveListener(StopCountDownBySec);
        GameManager.OnHasInputActive.RemoveListener(BeginCount);

    }

    private void BeginCount()
    {
        if (_coundownCorotine != null) StopCountDownBySec();
        _coundownCorotine = StartCoroutine(CountDownBySec(RoundTimeLimit));
    }
    void InitCount()
    {
        elapseTime = 0;
        currentTimeRemain = RoundTimeLimit;

        timerSlider.SetTimeSlider(RoundTimeLimit);
        countDownUI.SetTimeCount(RoundTimeLimit);
        StopCountDownBySec();//if count is runing disable it
    }
    void StopCountDownBySec()
    {
        if (_coundownCorotine != null)
            StopCoroutine(_coundownCorotine);
    }
    void Start()
    {

        InitCount();

    }
    void Update()
    {
        // currentTimeRemain -= Time.deltaTime;



    }
    IEnumerator CountDownBySec(float countdownSec)
    {

        while (currentTimeRemain >= 0 && _isCountdown == true)
        {

            currentTimeRemain -= Time.deltaTime;
            timerSlider.UpdateSlider(currentTimeRemain);
            countDownUI.UpdateCountdownDisplay(currentTimeRemain);
            yield return null;
        }
        GameManager.OnOutOfTime.Invoke();

    }

}
