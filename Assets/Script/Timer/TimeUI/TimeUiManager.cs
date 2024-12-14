using UnityEngine;

public class TimeUiManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TimerSlider timerSlider;
    [SerializeField] CountDownUI countDownUI;

    [SerializeField] float RoundTimeLimit;
    private float elapseTime;
    private float currentTimeRemain;
    void OnEnable()
    {
        GameManager.OnResetScene.AddListener(OnRestThisRound);
    }
    void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(OnRestThisRound);
    }
    private void OnRestThisRound()
    {
        elapseTime = 0;
        currentTimeRemain = RoundTimeLimit;

        timerSlider.SetTimeSlider(RoundTimeLimit);

        countDownUI.SetTimeCount(RoundTimeLimit);

    }

    void Start()
    {
        elapseTime = 0;
        currentTimeRemain = RoundTimeLimit;
        timerSlider.SetTimeSlider(RoundTimeLimit);

    }
    void Update()
    {
        currentTimeRemain -= Time.deltaTime;

        timerSlider.UpdateSlider(currentTimeRemain);
        countDownUI.UpdateCountdownDisplay(currentTimeRemain);
    }
}
