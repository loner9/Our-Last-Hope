using System;
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
    private Vector2 aimInput;
    public float moveSpeed = 5f;
    private float verticalVelocity;
    [SerializeField]
    private LayerMask aimLayerMask;
    [SerializeField]
    private Transform aim;
    private Vector3 lookingDirection;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Character.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Character.Movement.canceled += ctx => moveInput = Vector2.zero;

        controls.Character.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Character.Aim.canceled += ctx => aimInput = Vector2.zero;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyMovement();
        AimToMouse();
    }

    private void AimToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, aimLayerMask))
        {
            lookingDirection = hit.point - transform.position;
            lookingDirection.y = 0f;
            lookingDirection.Normalize();

            transform.forward = lookingDirection;

            aim.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
    }

    private void ApplyMovement()
    {
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        ApplyGravity();

        if (moveDirection.magnitude > 0)
        {
            characterController.Move(moveDirection * Time.deltaTime * moveSpeed);
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded){
            verticalVelocity -= 9.8f * Time.deltaTime;
            moveDirection.y = verticalVelocity;
        }else{
            verticalVelocity = -0.5f;
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
