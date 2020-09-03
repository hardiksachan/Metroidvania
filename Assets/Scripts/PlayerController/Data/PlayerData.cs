using UnityEngine;

namespace PlayerController.Data
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Move State")]
        public float movementSpeed = 10f;
        public float groundCheckRadius = 0.5f;
        public LayerMask whatIsGround;
        [Space]
        public float slopeCheckDistance = .5f;
        public PhysicsMaterial2D noFrictionMat;
        public PhysicsMaterial2D fullFrictionMat;
        
        [Header("Crouch State")]
        public float crouchMovementSpeed = 8f;
        public float crouchColliderScale = .5f;

        [Header("Jump State")]
        public float variableJumpHeightMultiplier = .5f;
        public int amountOfJumps = 1;
        public float jumpVelocity = 15f;
        public float coyoteTime = .1f;

        [Header("Wall State")]
        public float wallCheckDistance = .5f;
        public float wallSlideVelocity = 7f;
        public float wallClimbVelocity = 7f;
        
    }
}
