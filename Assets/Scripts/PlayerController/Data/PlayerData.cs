using UnityEngine;

namespace PlayerController.Data
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player/Player Data")]
    public class PlayerData : ScriptableObject
    {
        public float movementSpeed = 10f;
        public float groundCheckRadius = 0.5f;
        public LayerMask whatIsGround;
        
    }
}
