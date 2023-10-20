using UnityEngine;

namespace Player.PlayerData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        private LayerMaskCollision _layerMaskCollision;
        private PlayerInputMap _playerInput;

        public float crouchSizeReduction;

        [Header("Movement")]
        public float moveSpeed;
        public float acceleration;
        public float deceleration;
        public float velocityPower;
        public float frictionAmount;
        [Space]

        [Header("Jump")]
        public float jumpForce;
        public int extraJumps;
        [Range(0, 1)] public float jumpCutMultiplier;
        [Space] public float coyoteTime;
        public float jumpBufferTime;
        [Space] public float fallGravityMultiplier;
        public float maxFallSpeed;
        [Space] public float jumpApexThreshold;
        public float apexBonus;
    }
}