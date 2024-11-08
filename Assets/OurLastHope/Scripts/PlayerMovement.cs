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
    private const float StaminaDecreasePerFrame = 75.0f;
    private const float StaminaIncreasePerFrame = 15.0f;
    private float StaminaTimeToRegen = 3.0f;
    private Player player;
    private bool isRangedActive = false;
    private bool isMeleeActive = false;
    private bool isUnArmedActive = true;
    private bool isFiring = false;
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
            else
            {
                speed = moveSpeed;
                IsRunning = false;
            }

        };

        controls.Character.Fire.performed += ctx =>
        {
            if (!isUnArmedActive)
            {
                isFiring = true;
            }

        };
        controls.Character.Fire.canceled += ctx => isFiring = false;

        WeaponManager.OnWeaponStatusChanged += UpdateWeaponStatus;

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

        if (player.playerStaminas.CurrentStamina <= 0)
        {
            speed = moveSpeed;
            IsRunning = false;
        }

        if (moveDirection.magnitude > 0)
        {
            characterController.Move(moveDirection * Time.deltaTime * speed);

            if (IsRunning)
            {
                player.playerStaminas.UseStamina(StaminaDecreasePerFrame * Time.unscaledDeltaTime);
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
            speed = moveSpeed;
            IsRunning = false;
        }

        // player.StatsHid.stamina = Mathf.Clamp(player.StatsHid.stamina, 0.0f, player.StatsHid.maxStamina);
    }

    private void RegenerateStamina()
    {
        if (player.playerStaminas.CurrentStamina < player.StatsHid.maxStamina)
        {
            if (StaminaRegenTimer >= StaminaTimeToRegen)
            {
                player.playerStaminas.RecoverStaminaUpdate(StaminaIncreasePerFrame * Time.unscaledDeltaTime);
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

        if (isUnArmedActive)
        {
            animator.SetBool("toMelee", false);
            animator.SetBool("toRange", false);
            animator.SetBool("unArmed", true);
        }else if (isRangedActive)
        {
            animator.SetBool("toMelee", false);
            animator.SetBool("toRange", true);
            animator.SetBool("unArmed", false);
            animator.SetFloat("XVelocity", XVelocity, .1f, Time.deltaTime);
            animator.SetFloat("ZVelocity", ZVelocity, .1f, Time.deltaTime);
            animator.SetBool("gunIdle", isFiring);
            // if (!isFiring && moveDirection.magnitude == 0)
            // {
            //     animator.SetBool("gunIdle", false);
            // }
            // else if (!isFiring && moveDirection.magnitude > 0)
            // {
            //     animator.SetBool("gunWalk", false);
            // }
        }
        else if (isMeleeActive)
        {
            animator.SetBool("toMelee", true);
            animator.SetBool("toRange", false);
            animator.SetBool("unArmed", false);
            animator.SetFloat("XVelocity", XVelocity, .1f, Time.deltaTime);
            animator.SetFloat("ZVelocity", ZVelocity, .1f, Time.deltaTime);
            animator.SetBool("meleeWalk", isFiring && moveDirection.magnitude > 0);
            animator.SetBool("meleeIdle", isFiring && moveDirection.magnitude == 0);

            if (!isFiring && moveDirection.magnitude == 0)
            {
                animator.SetBool("meleeIdle", false);
            }
            else if (!isFiring && moveDirection.magnitude > 0)
            {
                animator.SetBool("meleeWalk", false);
            }

        }
    }

    private void UpdateWeaponStatus(bool isRanged, bool isMelee, bool isUnArmed)
    {
        isRangedActive = isRanged;
        isMeleeActive = isMelee;
        isUnArmedActive = isUnArmed;
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

    private void OnDestroy()
    {
        WeaponManager.OnWeaponStatusChanged -= UpdateWeaponStatus;
    }
}
