using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CommandScheduler
{
    [SerializeField] private Queue<ICommand> commands = new Queue<ICommand>();
    [SerializeField] public Queue<ICommand> replayCommand = new Queue<ICommand>();
    [SerializeField] public List<Queue<ICommand>> replayCommandsList = new();

    [SerializeField] private Queue<ICommand> replayCommandTemp = new();
    private int currentReplayIndex = -1;



    public void ScheduleCommand(ICommand command)
    {
        commands.Enqueue(command);
        replayCommandsList[currentReplayIndex].Enqueue(command);
    }
    public void CreateNewReplayQueue()
    {
        replayCommandTemp.Clear();
        Queue<ICommand> replay = new();
        replayCommandsList.Add(replay);
        currentReplayIndex++;
    }
    public void ClearCurrentReplay()
    {
        replayCommandsList[currentReplayIndex]?.Clear();
    }
    public void ReversePrevReplay()
    {
        replayCommandsList.RemoveAt(currentReplayIndex);
        currentReplayIndex--;

    }
    public void BeginExecuteReplay()
    {
        // Debug.Log(currentReplayIndex);
        replayCommandTemp = new Queue<ICommand>(replayCommandsList[currentReplayIndex]);

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
