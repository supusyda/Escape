using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandScheduler
{
    public static Queue<ICommand> commands = new Queue<ICommand>();
    public static Stack<ICommand> undoCommands = new Stack<ICommand>();
    public static Stack<ICommand> redoCommands = new Stack<ICommand>();
    public static Queue<ICommand> replayCommand = new Queue<ICommand>();


    public static void ScheduleCommand(ICommand command)
    {
        commands.Enqueue(command);
        replayCommand.Enqueue(command);
    }
    public static void Execute()
    {

        ICommand command = commands.Dequeue();
        undoCommands.Push(command);

        redoCommands.Clear();
        if (command != null)
        {
            command.Execute();
        }
    }
    public static void BeginExecuteReplay()
    {

    }
    public static void ExecuteReplay()
    {
        Debug.Log(replayCommand.Count);
        if (replayCommand.Count <= 0) return;
        ICommand command = replayCommand.Dequeue();
        if (command != null)
        {
            command.Execute();

        }
    }
    public static void Undo()
    {
        if (undoCommands.Count <= 0) return;

        ICommand command = undoCommands.Pop();
        redoCommands.Push(command);
        if (command != null)
        {
            command.Undo();
        }
    }
    public static void Redo()
    {
        if (redoCommands.Count <= 0) return;
        ICommand command = redoCommands.Pop();
        undoCommands.Push(command);
        if (command != null)
        {
            command.Execute();
        }
    }
    public static void Clear()
    {
        commands.Clear();
        undoCommands.Clear();
        redoCommands.Clear();
    }
}
