using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CommandScheduler
{
    [SerializeField] private Queue<ICommand> commands = new Queue<ICommand>();
    [SerializeField] public Queue<ICommand> replayCommand = new Queue<ICommand>();
    [SerializeField] private Queue<ICommand> replayCommandTemp = new();




    public void ScheduleCommand(ICommand command)
    {
        commands.Enqueue(command);
        replayCommand.Enqueue(command);
    }
    public void BeginExecuteReplay()
    {
        replayCommandTemp = new Queue<ICommand>(replayCommand);

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
