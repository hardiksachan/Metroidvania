using UnityEngine;

namespace FiniteStateMachine
{
    public class State
    {
        protected StateMachine stateMachine;

        protected float startTime;

        protected bool isExitingState;
        
        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public virtual void Enter()
        {
            startTime = Time.time;
            isExitingState = false;
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            
        }

        public virtual void Exit()
        {
            isExitingState = true;
        }
    }
}
