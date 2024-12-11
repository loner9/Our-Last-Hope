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
    public Vector2 moveInput { get; private set; }
    private Vector2 aimInput;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    [SerializeField] private float turnSpeed;
    private float speed;
    private float verticalVelocity;
    
    private bool IsRunning;
    private float StaminaRegenTimer = 0.0f;
    private const float StaminaDecreasePerFrame = 55.0f;
    private const float StaminaIncreasePerFrame = 35.0f;
    private float StaminaTimeToRegen = 0.5f;
    private Player player;
    private bool isRangedActive = false;
    private bool isMeleeActive = false;
    private bool isUnArmedActive = true;
    private bool isReloading = false;
    private bool isMeleeAttacking = false;
    private bool isMeleeAttackActive = false;
    private bool isFiring = false;

    [Header("Dialog | Dendy")]
    [SerializeField] private DialogueLevelUI dialogueUI;
    [SerializeField] private DialogueLevelUI dialogueUIExtended;
    public DialogueLevelUI DialogueUI => dialogueUI;
    public DialogueLevelUI DialogueUIExtended => dialogueUIExtended;
    public IInteractable Interactable { get; set; }

    private void Awake()
    {
        player = GetComponent<Player>();
        WeaponManager.OnWeaponStatusChanged += UpdateWeaponStatus;

    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();

        speed = moveSpeed;

        AssignInputEvents();
    }

    private void AssignInputEvents(){
        player.Controls.Character.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        player.Controls.Character.Movement.canceled += ctx => moveInput = Vector2.zero;

        player.Controls.Character.Run.performed += ctx =>
        {
            if (moveDirection.magnitude > 0 && player.StatsHid.stamina > 0)
            {
                speed = runSpeed;
                IsRunning = true;
            }

        };
        player.Controls.Character.Run.canceled += ctx =>
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

        player.Controls.Character.Fire.performed += ctx =>
        {
            if (!isUnArmedActive)
            {
                if (isRangedActive && !isReloading && !PauseManager.Instance.IsGamePaused)
                {
                    animator.SetTrigger("Fire");
                }
                else if (isMeleeActive && !isMeleeAttacking && !PauseManager.Instance.IsGamePaused)
                {
                    animator.SetTrigger("Fire");
                }
            }

        };
        player.Controls.Character.Fire.canceled += ctx => isFiring = false;
    }

    private void Update()
    {
        ApplyMovement();
        ApplyRotation();
        AnimatorController();

        if (dialogueUI == null) return;
        if (dialogueUI.IsOpen) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this);
            Debug.Log("E Jalan");
        }
    }

    private void ApplyRotation()
    {
        
        Vector3 lookingDirection = player.aim.GetMousePosition() - transform.position;
        lookingDirection.y = 0f;
        lookingDirection.Normalize();

        Quaternion desiredDirection = Quaternion.LookRotation(lookingDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredDirection, turnSpeed * Time.deltaTime);
        
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
            // animator.SetBool("meleeWalk", isFiring && moveDirection.magnitude > 0);
            animator.SetBool("meleeIdle", isFiring);

            // if (!isFiring && moveDirection.magnitude == 0)
            // {
            //     animator.SetBool("meleeIdle", false);
            // }
            // else if (!isFiring && moveDirection.magnitude > 0)
            // {
            //     animator.SetBool("meleeWalk", false);
            // }

        }
    }

    private void UpdateWeaponStatus(bool isRanged, bool isMelee, bool isUnArmed, bool isReload, bool isMeleeAttackActive)
    {
        isRangedActive = isRanged;
        isMeleeActive = isMelee;
        isUnArmedActive = isUnArmed;
        isReloading = isReload;
        isMeleeAttacking = isMeleeAttackActive;
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }

    private void OnDestroy()
    {
        WeaponManager.OnWeaponStatusChanged -= UpdateWeaponStatus;
    }
}
