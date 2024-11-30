using UnityEngine;

public class InputCommandJump : ICommand
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveInput;
    PlayerMoveBase _playerMoveBase;
    public InputCommandJump(PlayerMoveBase _playerMoveBase)
    {

        this._playerMoveBase = _playerMoveBase;
    }
    public void Execute()
    {
        _playerMoveBase.Jump();
    }

    public void Undo()
    {
        _playerMoveBase.Move(-moveInput);

    }
}
