using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine2 : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInput _playerInput;

    private Vector2 _currentMovementInput;
    private Vector2 _currentMovement;
    private Vector2 _appliedMovement;
    private bool _isMovementPressed;

    private bool _isJumpPressed;


    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rb = GetComponent<Rigidbody2D>();





        _playerInput.Gameplay.Walk.started += OnMomeventInput;
        _playerInput.Gameplay.Walk.canceled += OnMomeventInput;
        _playerInput.Gameplay.Walk.performed += OnMomeventInput;
        _playerInput.Gameplay.Jump.started += OnJumpInput;
        _playerInput.Gameplay.Jump.canceled += OnJumpInput;
        _playerInput.Gameplay.Jump.performed += OnJumpInput;
        _playerInput.Gameplay.Interact.started += OnInteractInput;
        _playerInput.Gameplay.Interact.canceled += OnInteractInput;
        _playerInput.Gameplay.Interact.performed += OnInteractInput;




    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMomeventInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        
    }

    private void OnEnable()
    {
        _playerInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Disable();
    }
}
