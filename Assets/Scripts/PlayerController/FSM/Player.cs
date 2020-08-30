using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.Input;
using PlayerController.PlayerStates.SubStates;
using UnityEngine;

namespace PlayerController.FSM
{
    public class Player : MonoBehaviour
    {
        #region StateMachine Variables

        public StateMachine StateMachine { get; private set; }

        public Player_IdleState IdleState { get; private set; }
        public Player_MoveState MoveState { get; private set; }
        public Player_CrouchIdleState CrouchIdleState { get; private set; }
        public Player_CrouchMoveState CrouchMoveState { get; private set; }
        public Player_SlopeIdleState SlopeIdleState { get; private set; }
        public Player_SlopeMoveState SlopeMoveState { get; private set; }
        public Player_JumpState JumpState { get; private set; }
        public Player_InAirState InAirState { get; private set; }
        public Player_LandState LandState { get; private set; }

        [SerializeField] private PlayerData playerData;

        #endregion

        #region Components

        public Animator Anim { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public CapsuleCollider2D Collider { get; private set; }

        #endregion

        #region Transforms

        [SerializeField] private Transform groundCheck;

        #endregion

        #region Slope Check Variables

        public bool IsOnSlope { get; private set; }

        private float _slopeDownAngle;
        private float _slopeDownAngleOld;
        private float _slopeSideAngle;

        public Vector2 SlopeDirection { get; private set; }

        #endregion

        #region Other Variables

        public Vector2 CurrentVelocity { get; private set; }
        public int FacingDirection { get; private set; }

        private Vector2 _workspace;
        private Vector2 _colliderInitialSize;
        private Vector2 _colliderInitialOffset;

        #endregion

        #region Unity Callback Functions

        private void Awake()
        {
            StateMachine = new StateMachine();
            IdleState = new Player_IdleState(StateMachine, "idle", this, playerData);
            MoveState = new Player_MoveState(StateMachine, "move", this, playerData);
            CrouchIdleState = new Player_CrouchIdleState(StateMachine, "crouchIdle", this, playerData);
            CrouchMoveState = new Player_CrouchMoveState(StateMachine, "crouchMove", this, playerData);
            SlopeIdleState = new Player_SlopeIdleState(StateMachine, "idle", this, playerData);
            SlopeMoveState = new Player_SlopeMoveState(StateMachine, "move", this, playerData);
            JumpState = new Player_JumpState(StateMachine, "inAir", this, playerData);
            InAirState = new Player_InAirState(StateMachine, "inAir", this, playerData);
            LandState = new Player_LandState(StateMachine, "land", this, playerData);
        }

        void Start()
        {
            InputHandler = GetComponent<PlayerInputHandler>();
            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();

            Collider = GetComponent<CapsuleCollider2D>();
            _colliderInitialSize = Collider.size;
            _colliderInitialOffset = Collider.offset;

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
        
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            Rb.velocity = _workspace;
            CurrentVelocity = _workspace;
        }

        public void SetColliderScale(bool crouched)
        {
            if (crouched)
            {
                var size = new Vector2();
                size.Set(_colliderInitialSize.x, _colliderInitialSize.y*playerData.crouchColliderScale);
                Collider.size = size;
                
                var offset = new Vector2();
                offset.Set(_colliderInitialOffset.x, _colliderInitialOffset.y-((_colliderInitialSize.y - size.y)/2));
                Collider.offset = offset;
            }
            else
            {
                Collider.size = _colliderInitialSize;
                Collider.offset = _colliderInitialOffset;
            }
        }


        #endregion

        #region Other Functions

        private void AnimationTrigger()
        {
            var state = (Player_BaseState) StateMachine.CurrentState;
            state.AnimationTrigger();
        }

        private void AnimtionFinishTrigger()
        {
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
            if (xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        public void CheckSlope()
        {
            Vector2 checkPos = transform.position - (Vector3) (new Vector2(0.0f, _colliderInitialSize.y / 2 * transform.localScale.y));

            // Check Horizontal 
            RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, playerData.slopeCheckDistance,
                playerData.whatIsGround);
            RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, playerData.slopeCheckDistance,
                playerData.whatIsGround);

            if (slopeHitFront)
            {
                IsOnSlope = true;

                _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            }
            else if (slopeHitBack)
            {
                IsOnSlope = true;

                _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            }
            else
            {
                _slopeSideAngle = 0.0f;
                IsOnSlope = false;
            }


            // Check Vertical
            RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, playerData.slopeCheckDistance,
                playerData.whatIsGround);

            if (hit)
            {
                SlopeDirection = -Vector2.Perpendicular(hit.normal).normalized;

                _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (_slopeDownAngle != _slopeDownAngleOld)
                {
                    IsOnSlope = true;
                }

                _slopeDownAngleOld = _slopeDownAngle;

                Debug.DrawRay(hit.point, SlopeDirection, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.green);
            }
            
            // TODO: slope max angle
        }

        #endregion
    }
}