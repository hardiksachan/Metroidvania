﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 RawMovementInput { get; private set; }
        public int NormInputX { get; private set; }
        public int NormInputY { get; private set; }
        public bool JumpInput { get; private set; }
        public bool JumpInputStop { get; private set; }
        public bool CrouchInput { get; private set; }

        [SerializeField]
        private float inputHoldTime = 0.2f;

        private float _jumpInputStartTime;


        private void Update()
        {
            CheckJumpInputHoldTime();
        }


        public void OnMoveInput(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();

            if(Mathf.Abs(RawMovementInput.x) > 0.5f)
            {
                NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            }
            else
            {
                NormInputX = 0;
            }
        
            if(Mathf.Abs(RawMovementInput.y) > 0.5f)
            {
                NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
            }
            else
            {
                NormInputY = 0;
            }
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                JumpInput = true;
                JumpInputStop = false;
                _jumpInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                JumpInputStop = true;
            }
        }

        public void OnCrouchInput(InputAction.CallbackContext context)
        {
            if (context.started) CrouchInput = true;
            if (context.canceled) CrouchInput = false;
        }
        
        public void UseJumpInput() => JumpInput = false;

        private void CheckJumpInputHoldTime()
        {
            if(Time.time >= _jumpInputStartTime + inputHoldTime)
            {
                JumpInput = false;
            }
        }
    }
}
