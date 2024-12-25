using System;
using System.Collections;
using UnityEngine;
public enum RecordState
{
    None,
    Record,
    Replay
}
public class PlayerRecord : MonoBehaviour
{
    public RecordState recordState;
    [SerializeField] private CommandScheduler commandScheduler = new();
    private PlayerUI _playerUI;
    private Collider2D _playerCollider;
    PlayerBase _playerBase;


    void OnEnable()
    {
        // GameManager.OnResetScene.AddListener(OnRestThisRound);
        GameManager.OnEndRound.AddListener(OnRestThisRound);
        // GameManager.OnHasInputActive.AddListener(OnHasInputActive);

    }


    void OnDisable()
    {
        // GameManager.OnResetScene.RemoveListener(OnRestThisRound);
        GameManager.OnEndRound.RemoveListener(OnRestThisRound);
    }
    void Start()
    {
        _playerBase = GetComponent<PlayerBase>();
        _playerUI = _playerBase.playerUI; // get playerUI component;
        _playerCollider = _playerBase.playerCollider;
        ChangeState(RecordState.None);
    }
    void OnRestThisRound()
    {

        ChangeState(RecordState.None);
    }
    public void AddRecord(ICommand command)
    {

        if (recordState != RecordState.Record) return;
        commandScheduler.ScheduleCommand(command);
        commandScheduler.Execute();
    }
    public void ChangeState(RecordState newState)
    {
        switch (newState)
        {
            case RecordState.Record:
                if (commandScheduler != null)
                {
                    // commandScheduler.Clear();
                }
                commandScheduler.CreateNewReplayQueue();

                recordState = RecordState.Record;

                _playerBase.Active(); // active the colider and layer of this player


                _playerUI.ShowControlPlayerIndicator();//show control player indicator

                break;
            case RecordState.Replay:

                recordState = RecordState.Replay;
                commandScheduler.BeginExecuteReplay();

                _playerBase.Active();// active the colider and layer of this player
                _playerUI.HideControlPlayerIndicator();
                break;
            case RecordState.None:
                recordState = RecordState.None;

                _playerBase.DeactivePlayer();

                // StartCoroutine(CallWithDelay(.1f, () => { _playerBase.DeactivePlayer(); }));// delay deactive to avoid collision not working correctly

                _playerUI.HideControlPlayerIndicator(); // hide control player indicator

                break;
            default:
                break;

        }

    }
    public void ClearRecord()
    {

        commandScheduler.ClearCurrentReplay();
    }
    void FixedUpdate()
    {
        if (recordState == RecordState.Replay)
        {
            commandScheduler.ExecuteReplay();
            if (commandScheduler.DoneReplay())
            {
                StartCoroutine(CallWithDelay(.2f, () => { ChangeState(RecordState.None); })); ;
            }
        }

    }
    IEnumerator CallWithDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

}
