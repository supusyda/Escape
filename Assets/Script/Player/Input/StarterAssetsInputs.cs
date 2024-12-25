using System;
using UnityEngine;
using UnityEngine.Events;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public bool jump;

        public bool shoot;
        public bool openMenu;



        public static UnityEvent OnOpenMenuPress = new();




#if ENABLE_INPUT_SYSTEM

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());

        }



        public void OnJump(InputValue value)
        {

            JumpInput(value.isPressed);
        }


        public void OnMenuOpen(InputValue value)
        {
            OnOpenMenuPress.Invoke();
        }
        public void OnShoot(InputValue value)
        {
            ShootInput(value.isPressed);
        }

#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;

        }



        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }



        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
        private void ShootInput(bool isPressed)
        {

            shoot = isPressed;
        }

    }

}