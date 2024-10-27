using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveDirection;
    private PlayerControls controls;
    private Vector2 moveInput;
    private Vector2 aimInput;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    private float speed;
    private float verticalVelocity;
    [SerializeField]
    private LayerMask aimLayerMask;
    [SerializeField]
    private Transform aim;
    private Vector3 lookingDirection;
    private bool IsRunning;
    private float StaminaRegenTimer = 0.0f;
    private const float StaminaDecreasePerFrame = 55.0f;
    private const float StaminaIncreasePerFrame = 25.0f;
    private float StaminaTimeToRegen = 3.0f;
    private Player player;

    private void Awake()
    {
        controls = new PlayerControls();

        player = GetComponent<Player>();

        controls.Character.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Character.Movement.canceled += ctx => moveInput = Vector2.zero;

        controls.Character.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Character.Aim.canceled += ctx => aimInput = Vector2.zero;

        controls.Character.Run.performed += ctx =>
        {
            if (moveDirection.magnitude > 0 && player.StatsHid.stamina > 0)
            {
                speed = runSpeed;
                IsRunning = true;
            }

        };
        controls.Character.Run.canceled += ctx =>
        {
            if (moveDirection.magnitude > 0)
            {
                speed = moveSpeed;
                IsRunning = false;
            }

        };

    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();

        speed = moveSpeed;
    }

    private void Update()
    {
        ApplyMovement();
        AimToMouse();
        AnimatorController();
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

        if (player.StatsHid.stamina <= 0)
        {
            speed = moveSpeed; 
            IsRunning = false;
        }

        if (moveDirection.magnitude > 0)
        {
            characterController.Move(moveDirection * Time.deltaTime * speed);
            
            if (IsRunning)
            {
                player.StatsHid.stamina -= StaminaDecreasePerFrame * Time.unscaledDeltaTime;
                StaminaRegenTimer = 0.0f;
            }
            else
            {
                RegenerateStamina();
            }
        }
        else
        {
            RegenerateStamina();
        }
        
        player.StatsHid.stamina = Mathf.Clamp(player.StatsHid.stamina, 0.0f, player.StatsHid.maxStamina);
    }

    private void RegenerateStamina()
    {
        if (player.StatsHid.stamina < player.StatsHid.maxStamina)
        {
            if (StaminaRegenTimer >= StaminaTimeToRegen)
            {
                player.StatsHid.stamina += StaminaIncreasePerFrame * Time.unscaledDeltaTime;
            }
            else
            {
                StaminaRegenTimer += Time.deltaTime;
            }
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= 9.8f * Time.deltaTime;
            moveDirection.y = verticalVelocity;
        }
        else
        {
            verticalVelocity = -0.5f;
        }
    }

    private void AnimatorController()
    {
        float XVelocity = Vector3.Dot(moveDirection.normalized, transform.right);
        float ZVelocity = Vector3.Dot(moveDirection.normalized, transform.forward);

        animator.SetFloat("XVelocity", XVelocity, .1f, Time.deltaTime);
        animator.SetFloat("ZVelocity", ZVelocity, .1f, Time.deltaTime);
        animator.SetBool("IsRunning", IsRunning);
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
