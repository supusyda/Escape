using UnityEngine;

public class InputCommandMove : ICommand
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveInput;
    PlayerMoveBase _playerMoveBase;
    Transform transform;
    public InputCommandMove(float moveInput, PlayerMoveBase _playerMoveBase)
    {
        this.moveInput = moveInput;
        this._playerMoveBase = _playerMoveBase;
        transform = _playerMoveBase.transform;

    }
    public void Execute()
    {
        _playerMoveBase.Move(moveInput);
        if (moveInput > 0) // Moving right
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput < 0) // Moving left
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Undo()
    {
        _playerMoveBase.Move(-moveInput);

    }
}
