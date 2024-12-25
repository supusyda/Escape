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
    private bool _canMove = true;


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

        GameManager.OnResetScene.AddListener(ResetMove);
        GameManager.OnHasInputActive.AddListener(HasInputActive);
    }

    private void HasInputActive()
    {
        _canMove = true;

    }

    void OnDisable()
    {

        GameManager.OnResetScene.AddListener(ResetMove);
        GameManager.OnHasInputActive.RemoveListener(HasInputActive);

    }
    private void ResetMove()
    {

        _playerBase.rigidbody2D.linearVelocity = Vector2.zero;

        currentVelocityX = 0;
        _input.jump = false;
        _canMove = false;


    }

    void FixedUpdate()
    {
        if (!_canMove) return;
        if (_playerRecord)
        {

            _playerRecord.AddRecord(new InputCommandMove(_input.move.x, this));// record the input
        }
        else
        {
            Move(_input.move.x);
            if (_input.move.x > 0) // Moving right
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (_input.move.x < 0) // Moving left
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        }
        HandleJump();// issue when press jump all the gameobject has the input will set _input.jump = true
        HandleGravity();
    }
    public void Move(float moveInput)
    {

        float moveSpeed = playerStat.maxMoveSpeed;
        if (_playerBase.wallCheck?.IsWalled == true) { currentVelocityX = 0; return; }
        if (moveInput != 0)
        {
            // Accelerate towards the target speed
            _playerBase.playerAnim?.ChangeAnimatorState(PlayerAnimateState.Walk);
            currentVelocityX = Mathf.Lerp(currentVelocityX, moveInput * moveSpeed, playerStat.accelerateSpeed * Time.fixedDeltaTime);
            // currentVelocityX = Mathf.MoveTowards(currentVelocityX, moveInput * moveSpeed, playerStat.accelerateSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate to a stop when no input is given
            currentVelocityX = Mathf.Lerp(currentVelocityX, 0, playerStat.decelerateSpeedTime * Time.fixedDeltaTime);
            // currentVelocityX = Mathf.MoveTowards(currentVelocityX, 0, playerStat.decelerateSpeedTime * Time.fixedDeltaTime);


        }


        _playerBase.rigidbody2D.linearVelocityX = currentVelocityX;
    }
    private void HandleJump()
    {
        if (!_groundCheck.IsGrounded) return;

        if (_input.jump == true)
        {
            if (_playerRecord)
            {
                _playerRecord.AddRecord(new InputCommandJump(this));
            }
            else
            {
                Jump();
            }
        }

    }
    public void Jump()
    {
        float jumpForce = Mathf.Sqrt(playerStat.JumpHeight * (Physics2D.gravity.y * _playerBase.rigidbody2D.gravityScale) * -2);
        _playerBase.rigidbody2D.AddForce(new Vector2(_playerBase.rigidbody2D.linearVelocityX, jumpForce), ForceMode2D.Impulse);
        // Debug.Log("JUMP ASS" + transform.name);  
        _input.jump = false;
        _playerBase.playerAnim?.ChangeAnimatorState(PlayerAnimateState.Jump);
        ParticalSpawner.OnSpawnPartical?.Invoke(transform.localScale, _playerBase.footPosition.position, ParticleDefine.JUMP_PARTICLE);
        AudioManager.Instance?.PlaySound(AudioClipName.SFX_JUMP);

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

    public void PlayMoveParticle()
    {
        if (_playerBase.footPosition && _groundCheck.IsGrounded)
        {

            ParticalSpawner.OnSpawnPartical.Invoke(transform.localScale, _playerBase.footPosition.position, ParticleDefine.RUN_PARTICLE);
        }
    }

}
