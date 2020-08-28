using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using UnityEngine;

public class Player_GroundedState : Player_BaseState
{
    protected int InputX;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _isGrounded;
    private bool _isTouchingWall;
    
    public Player_GroundedState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerPlayerData) : base(stateMachine, animBoolName, player, playerPlayerData)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        InputX = player.InputHandler.NormInputX;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        
    }
}
