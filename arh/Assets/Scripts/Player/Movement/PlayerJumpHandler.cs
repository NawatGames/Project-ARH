using System;
using Player.Movement;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerJumpHandler : MonoBehaviour
    {
        [Header("Jump")]
        [SerializeField] private Rigidbody2D rb;
        // [SerializeField] private bool canDoubleJump;
        [SerializeField] private float jumpForce;
        [Range(0, 1)] [SerializeField] private float jumpCutMultiplier;
        // [SerializeField] private float maxFallSpeed;
        
        [SerializeField] private JumpRequester jumpRequester;

        private void OnEnable()
        {
            jumpRequester.JumpStartedEvent.AddListener(OnJumpStarted);
            jumpRequester.JumpCanceledEvent.AddListener(OnJumpCanceled);
        }

        private void OnDisable()
        {
            jumpRequester.JumpStartedEvent.RemoveListener(OnJumpStarted);
            jumpRequester.JumpCanceledEvent.RemoveListener(OnJumpCanceled);
        }

        private void OnJumpStarted()
        {
            Jump();
        }

        private void OnJumpCanceled()
        {
            JumpCancel();
        }

        private void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        private void JumpCancel()
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);

        }
    }
}