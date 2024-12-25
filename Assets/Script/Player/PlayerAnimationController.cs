using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum PlayerAnimateState
{
    Idle, Walk, Jump, Fall, Hurt, Die

}
public class PlayerAnimationController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerBase _playerBase;
    private static readonly Dictionary<PlayerAnimateState, int> animationHashes = new()
    {
        { PlayerAnimateState.Idle, Animator.StringToHash(PlayerAnimateState.Idle.ToString()) },
        { PlayerAnimateState.Walk, Animator.StringToHash(PlayerAnimateState.Walk.ToString()) },
        { PlayerAnimateState.Jump, Animator.StringToHash(PlayerAnimateState.Jump.ToString()) },
        { PlayerAnimateState.Fall, Animator.StringToHash(PlayerAnimateState.Fall.ToString()) },
        { PlayerAnimateState.Hurt, Animator.StringToHash(PlayerAnimateState.Hurt.ToString()) },
        { PlayerAnimateState.Die, Animator.StringToHash(PlayerAnimateState.Die.ToString()) }

    };
    private PlayerAnimateState _currentAnimatorState;
    Animator animator;
    void OnEnable()
    {
        GameManager.OnResetScene.AddListener(OnResetScene);
    }

    private void OnResetScene()
    {
        ChangeAnimatorState(PlayerAnimateState.Idle);
    }

    void OnDisable()
    {
        GameManager.OnResetScene.RemoveListener(OnResetScene);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        _playerBase = GetComponent<PlayerBase>();
        ChangeAnimatorState(PlayerAnimateState.Idle);
    }
    void Update()
    {
        HandleCurrentAnimatorStateUpdate();

    }
    void HandleCurrentAnimatorStateUpdate()
    {
        // if (_currentAnimatorState == PlayerAnimateState.)
        switch (_currentAnimatorState)

        {
            case PlayerAnimateState.Idle:

                break;
            case PlayerAnimateState.Walk:

                if (_playerBase.rigidbody2D.linearVelocityX <= .1f && _playerBase.rigidbody2D.linearVelocityX >= -.1f)
                    ChangeAnimatorState(PlayerAnimateState.Idle);


                break;

            case PlayerAnimateState.Jump:
                if (_playerBase.rigidbody2D.linearVelocityY < 0)
                    ChangeAnimatorState(PlayerAnimateState.Fall);



                break;

            case PlayerAnimateState.Fall:
                if (_playerBase.rigidbody2D.linearVelocityY == 0)
                    ChangeAnimatorState(PlayerAnimateState.Idle);

                break;

            case PlayerAnimateState.Hurt:
                if (IsAnimationFinished(GetHashForState(_currentAnimatorState)))
                {
                    ChangeAnimatorState(PlayerAnimateState.Die);
                }
                break;


            default:
                break;
        }

    }

    public void ChangeAnimatorState(PlayerAnimateState newState)
    {
        if (newState == _currentAnimatorState) return;
        switch (newState)
        {
            case PlayerAnimateState.Idle:

                break;
            case PlayerAnimateState.Walk:

                break;
        }
        SmothTransistion(GetHashForState(newState));
        _currentAnimatorState = newState;
    }
    public void SmothTransistion(int newHashAnim)
    {
        animator.CrossFade(newHashAnim, 0, 0);
    }
    public static int GetHashForState(PlayerAnimateState state)
    {
        // Debug.Log(state);
        return animationHashes[state];
    }
    bool IsAnimationFinished(int animHash)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 is the base layer
        return stateInfo.shortNameHash == animHash && stateInfo.normalizedTime >= 1f;
    }
}
