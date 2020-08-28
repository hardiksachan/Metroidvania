﻿using System;
using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.Input;
using PlayerController.PlayerStates.SubStates;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region StateMachine Variables

    public StateMachine StateMachine { get; private set; }
    
    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_InAirState InAirState { get; private set; }
    public Player_LandState LandState { get; private set; }

    [SerializeField] private PlayerData playerData;

    #endregion

    #region Components

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    #endregion

    #region Transforms

    [SerializeField] private Transform groundCheck;

    #endregion
    
    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }    

    private Vector2 _workspace;
    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        StateMachine = new StateMachine();
        IdleState = new Player_IdleState(StateMachine, "idle", this, playerData);
        MoveState = new Player_MoveState(StateMachine, "move", this, playerData);
        JumpState = new Player_JumpState(StateMachine, "inAir", this, playerData);
        InAirState = new Player_InAirState(StateMachine, "inAir", this, playerData);
        LandState = new Player_LandState(StateMachine, "land", this, playerData);
    }

    void Start()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        
        StateMachine.Initialize(IdleState);

        FacingDirection = 1;
    }

    void Update()
    {
        CurrentVelocity = Rb.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        Rb.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    
    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        Rb.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    #endregion
    
    #region Other Functions

    private void AnimationTrigger()
    {
        var state = (Player_BaseState) StateMachine.CurrentState;
        state.AnimationTrigger();
    }

    private void AnimtionFinishTrigger() {
        var state = (Player_BaseState) StateMachine.CurrentState;
        state.AnimationFinishTrigger();
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
    
    #region Check Functions

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion
}