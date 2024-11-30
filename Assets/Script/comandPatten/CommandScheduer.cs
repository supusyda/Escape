using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandScheduler
{
    private Queue<ICommand> commands = new Queue<ICommand>();
    private Queue<ICommand> replayCommand = new Queue<ICommand>();
    private Queue<ICommand> replayCommandTemp = new();



    public void ScheduleCommand(ICommand command)
    {
        commands.Enqueue(command);
        replayCommand.Enqueue(command);
    }
    public void BeginExecuteReplay()
    {
        replayCommandTemp = new Queue<ICommand>(replayCommand);
        Debug.Log("replayCommandTemp" + "WHY ZERO");
        Debug.Log(replayCommandTemp.Count);
    }
    public void Execute()
    {

        ICommand command = commands.Dequeue();


        if (command != null)
        {
            command.Execute();
        }
    }
    public void ExecuteReplay()
    {

        if (replayCommandTemp.Count <= 0) return;
        ICommand command = replayCommandTemp.Dequeue();
        if (command != null)
        {
            command.Execute();

        }
    }
    public bool DoneReplay()
    {
        if (replayCommandTemp.Count <= 0) return true;
        return false;
    }
    public void Clear()
    {
        commands.Clear();
        replayCommand.Clear();
        replayCommandTemp.Clear();

    }
}
