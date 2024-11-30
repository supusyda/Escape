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
    private CommandScheduler commandScheduler = new();

    void Awake()
    {
        ChangeState(RecordState.Record);
    }
    public void AddRecord(ICommand command)
    {
        if (recordState != RecordState.Record) return;
        commandScheduler.ScheduleCommand(command);
        commandScheduler.Execute();
    }
    void ChangeState(RecordState newState)
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
                commandScheduler.BeginExecuteReplay();
                recordState = RecordState.Replay;
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeState(RecordState.Replay);
        }
    }


}
