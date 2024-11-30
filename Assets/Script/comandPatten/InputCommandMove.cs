using UnityEngine;

public class InputCommandMove : ICommand
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveInput;
    PlayerMoveBase _playerMoveBase;
    public InputCommandMove(float moveInput, PlayerMoveBase _playerMoveBase)
    {
        this.moveInput = moveInput;
        this._playerMoveBase = _playerMoveBase;
    }
    public void Execute()
    {
        _playerMoveBase.Move(moveInput);
    }

    public void Undo()
    {
        _playerMoveBase.Move(-moveInput);

    }
}
