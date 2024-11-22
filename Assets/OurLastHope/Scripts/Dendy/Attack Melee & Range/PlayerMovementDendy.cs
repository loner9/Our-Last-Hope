using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementDendy : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
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
    private bool isRangedActive = true; // Status senjata aktif
    private bool isFiring = false; // Status tembakan
    

    private bool isMoving = false; // Status gerakan pemain
    private bool isAttack = false;

    [Header("Dialog | Dendy")]
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Character.Movement.canceled += ctx => moveInput = Vector2.zero;
        controls.Character.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Character.Aim.canceled += ctx => aimInput = Vector2.zero;
        controls.Character.Fire.performed += ctx => isFiring = true;
        controls.Character.Fire.canceled += ctx => isFiring = false;

        // WeaponManager.OnWeaponStatusChanged += UpdateWeaponStatus;
        
    }

    private void OnDestroy()
    {
        // WeaponManager.OnWeaponStatusChanged -= UpdateWeaponStatus;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ApplyMovement();
        AimToMouse();
        AnimatorController();
        PlayFootstepSound();

        if (dialogueUI.IsOpen) return;
        if (Input.GetKeyDown(KeyCode.I))
        {
            Interactable?.Interact(this);
            Debug.Log("I Jalan");
        }
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
            if (!isMoving)
            {
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
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

        if (isRangedActive)
        {
            animator.SetBool("toMelee", false);
            animator.SetFloat("XVelocity", XVelocity, .1f, Time.deltaTime);
            animator.SetFloat("ZVelocity", ZVelocity, .1f, Time.deltaTime);
            animator.SetBool("gunWalk", isFiring && moveDirection.magnitude > 0);
            animator.SetBool("gunIdle", isFiring && moveDirection.magnitude == 0);
            animator.SetBool("meleeWalk", false);
            animator.SetBool("meleeIdle", false);
            if (!isFiring && moveDirection.magnitude == 0)
            {
                animator.SetBool("gunIdle", false);
            }
            else if (!isFiring && moveDirection.magnitude > 0)
            {
                animator.SetBool("gunWalk", false);
            }
        }
        else
        {
            animator.SetBool("toMelee", true);
            animator.SetFloat("X2Velocity", XVelocity, .1f, Time.deltaTime);
            animator.SetFloat("Z2Velocity", ZVelocity, .1f, Time.deltaTime);
            animator.SetBool("meleeWalk", isFiring && moveDirection.magnitude > 0);
            animator.SetBool("meleeIdle", isFiring && moveDirection.magnitude == 0);
            animator.SetBool("gunWalk", false);
            animator.SetBool("gunIdle", false);
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

    private void PlayFootstepSound()
    {
        
        
            
        
    }


    // private void UpdateWeaponStatus(bool isRanged)
    // {
    //     isRangedActive = isRanged;
    // }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
