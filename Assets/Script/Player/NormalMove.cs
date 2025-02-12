using System;
using System.Runtime.InteropServices;
using StarterAssets;
using UnityEngine;

public class NormalMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlayerStat playerStat;
    public Vector2 lookDir { get; private set; } = Vector2.zero;
    private StarterAssetsInputs _input;
    private PlayerBase _playerBase;
    private Transform _transform;
    private GroundCheck _groundCheck;

    private float currentVelocityX = 0;
    private PlayerRecord _playerRecord;
    private bool _canMove = false;


    void Start()
    {
        _playerBase = GetComponent<PlayerBase>();
        _input = GetComponent<StarterAssetsInputs>();
        _transform = this.transform;

        _groundCheck = _playerBase.groundCheck;


    }

    private void HasInputActive()
    {
        _canMove = true;

    }




    void FixedUpdate()
    {




        // _playerRecord.AddRecord(new InputCommandMove(_input.move.x, this));// record the input
        Move(_input.move.x);
        HandleJump();// issue when press jump all the gameobject has the input will set _input.jump = true
        HandleGravity();
    }
    public void Move(float moveInput)
    {

        float moveSpeed = playerStat.maxMoveSpeed;

        if (moveInput != 0)
        {
            // Accelerate towards the target speed
            currentVelocityX = Mathf.MoveTowards(currentVelocityX, moveInput * moveSpeed, playerStat.accelerateSpeed * Time.fixedDeltaTime);
            SetLookDir(new Vector2(moveInput, 0));
        }
        else
        {
            // Decelerate to a stop when no input is given
            currentVelocityX = Mathf.MoveTowards(currentVelocityX, 0, playerStat.decelerateSpeedTime * Time.fixedDeltaTime);
        }


        _playerBase.rigidbody2D.linearVelocity = new Vector2(currentVelocityX, _playerBase.rigidbody2D.linearVelocity.y);


    }
    private void HandleJump()
    {
        if (!_groundCheck.IsGrounded) return;

        if (_input.jump == true)
        {
            Jump();
        }

    }
    public void Jump()
    {
        float jumpForce = Mathf.Sqrt(playerStat.JumpHeight * (Physics2D.gravity.y * _playerBase.rigidbody2D.gravityScale) * -2);
        _playerBase.rigidbody2D.AddForce(new Vector2(_playerBase.rigidbody2D.linearVelocityX, jumpForce), ForceMode2D.Impulse);
        // Debug.Log("JUMP ASS" + transform.name);  
        _input.jump = false;

    }
    void HandleGravity()
    {
        if (!_groundCheck.IsGrounded && _playerBase.rigidbody2D.linearVelocityY < 0)
        {
            _playerBase.rigidbody2D.gravityScale = playerStat.fallGravityScale;
        }
        else
        {
            _playerBase.rigidbody2D.gravityScale = playerStat.gravityScale;
        }
    }
    void SetLookDir(Vector2 lookDir)
    {
        if (lookDir.x == 0) return;
        this.lookDir = lookDir;
    }

}
