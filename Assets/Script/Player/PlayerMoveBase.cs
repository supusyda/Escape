using System;
using System.Runtime.InteropServices;
using StarterAssets;
using UnityEngine;

public class PlayerMoveBase : MonoBehaviour
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


    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _transform = this.transform;
        _playerBase = GetComponent<PlayerBase>();
        _groundCheck = _playerBase.groundCheck;
        _playerRecord = _playerBase.playerRecord;

    }
    void OnEnable()
    {

        GameManager.OnResetScene.AddListener(ResetVelocity);
    }
    void OnDisable()
    {

        GameManager.OnResetScene.AddListener(ResetVelocity);

    }
    private void ResetVelocity()
    {

        _playerBase.rigidbody2D.linearVelocity = Vector2.zero;

        currentVelocityX = 0;
        _input.jump = false;

    }

    void FixedUpdate()
    {
        if (_playerRecord.recordState != RecordState.Record) return;
        _playerRecord.AddRecord(new InputCommandMove(_input.move.x, this));
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
            _playerRecord.AddRecord(new InputCommandJump(this));
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
