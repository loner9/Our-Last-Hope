using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;
    private PlayerControls controls;
    private Vector2 moveInput;
    [SerializeField]
    public float moveSpeed = 5f;

    private void Awake()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        controls = new PlayerControls();

        controls.Character.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Character.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        
        if (moveDirection.magnitude > 0)
        {
            characterController.Move(moveDirection * Time.deltaTime * moveSpeed);
        }
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
