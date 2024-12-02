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

    void Start()
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
                    commandScheduler.Clear();
                }
                recordState = RecordState.Record;
                break;
            case RecordState.Replay:
                Debug.Log("CHANGE STATE TO REPLAY");
                recordState = RecordState.Replay;
                commandScheduler.BeginExecuteReplay();
                break;
            case RecordState.None:
                recordState = RecordState.None;
                break;
            default:
                break;

        }

    }
    void FixedUpdate()
    {
        if (recordState == RecordState.Replay)
        {
            commandScheduler.ExecuteReplay();
            if (commandScheduler.DoneReplay())
            {
                ChangeState(RecordState.None);
            }
        }

    }


}
