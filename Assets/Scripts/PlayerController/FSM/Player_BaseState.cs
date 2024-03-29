﻿using FiniteStateMachine;
using PlayerController.Data;
using UnityEngine;

namespace PlayerController.FSM
{
    public class Player_BaseState : State
    {

        protected Player player;
        protected PlayerData PlayerData;

        protected bool IsAnimationFinished;

        private string _animBoolName;

        public Player_BaseState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine)
        {
            _animBoolName = animBoolName;
            this.player = player;
            PlayerData = playerData;
        }

        public override void Enter()
        {
            base.Enter();

            IsAnimationFinished = false;
            player.Anim.SetBool(_animBoolName, true);
            
            Debug.Log(_animBoolName);
            
            DoChecks();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            DoChecks();
        }

        public override void Exit()
        {
            base.Exit();
            player.Anim.SetBool(_animBoolName, false);
        }

        public virtual void DoChecks()
        {
            
        }
        
        public virtual void AnimationTrigger() { }

        public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;
    }
}
