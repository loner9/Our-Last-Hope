using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    public static System.Action<bool, bool, bool, bool> OnWeaponStatusChanged;
    public static Action<string> OnWeaponTypeChanged;
    public static Action<string, int, int, float> OnWeaponChanged;
    public static Action<Transform> OnFirePointChanged;
    public static Action<int> OnWeaponFired;
    private PlayerControls controls;
    [SerializeField] private GameObject bulletPrefab;
    private Transform firePoint;
    [SerializeField] private GameObject meleeWeapon;
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private AudioClip rangedAttackSound; // Suara tembakan
    [SerializeField] private AudioClip meleeAttackSound; // Suara serangan melee
    private AudioSource audioSource; // Sumber audio
    private bool canPlaySound = true;
    private float soundCooldown = 0.2f; // Waktu jeda antara suara
    private float soundTimer = 0f;

    private bool isRangedActive = false;
    private bool isMeleeActive = false;
    private bool isUnArmed = true;
    private bool isReloading = false;
    public static bool isMeleeAttackActive = false;
    [SerializeField] private Rig rig;
    [SerializeField] private Animator animator;
    private string weaponId;
    private int weaponMags;
    private int weaponIndex;
    private float weaponDamage;

    [SerializeField] private GameObject muzzleFlashPrefab;

    private void Awake()
    {
        controls = new PlayerControls();
        // controls.Character.SwitchToMelee.performed += ctx => SwitchToMelee();
        // controls.Character.SwitchToRanged.performed += ctx => SwitchToRanged();
        // controls.Character.Unarmed.performed += ctx => SwitchToUnarmed();
    }

    private void Start()
    {
        controls.Character.Fire.performed += ctx =>
        {
            if (isRangedActive && !isReloading)
            {
                Fire();
            }
        };
        controls.Character.Reload.performed += ctx => Reload();
        
        if (rig != null)
        {
            rig.weight = 0f;

        }
        OnWeaponTypeChanged += SetWeaponType;
        OnWeaponChanged += WeaponDetail;
        OnFirePointChanged += SetFirePoint;
    }

    private void SetFirePoint(Transform transform)
    {
        firePoint = transform;
    }

    private void Update()
    {
        if (isRangedActive)
        {
            if (isReloading)
            {
                rig.weight = Mathf.MoveTowards(rig.weight, 0f, 2f * Time.deltaTime);
            }
            else
            {
                rig.weight = Mathf.MoveTowards(rig.weight, 1f, 4f * Time.deltaTime);
            }
        }
    }

    private void SetWeaponType(string obj)
    {
        if (obj.ToLower().Equals("melee"))
        {
            SwitchToMelee();
        }
        else if (obj.ToLower().Equals("ranged"))
        {
            SwitchToRanged();
        }
        else
        {
            SwitchToUnarmed();
        }
    }

    private void WeaponDetail(string id, int mag, int index, float dmg)
    {
        weaponId = id;
        weaponMags = mag;
        weaponIndex = index;
        weaponDamage = dmg;
    }

    private void Fire()
    {
        if (isRangedActive && !isUnArmed)
        {
            int ammo = Inventory.Instance.ConsumeAmmo(weaponIndex);
            Debug.Log("Ammo: " + ammo);
            if (ammo != 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                bulletScript.damage = weaponDamage;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = firePoint.forward * 20f;

                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
                Destroy(muzzleFlash, 0.2f);
            }

        }
        else if (isMeleeActive && !isUnArmed)
        {
            Debug.Log("Fire Melee Weapon");
            isMeleeAttackActive = true;
            // Reset after a brief moment
            Invoke("ResetMeleeAttack", 0.5f);
        }
        else
        {
            Debug.Log("Unarmed");
        }
    }

    private void Reload()
    {
        if (!isRangedActive) return;
        if (isReloading) return;
        List<int> indexes = Inventory.Instance.CheckAmmoAvailable(weaponId);
        if (indexes.Count == 0) return;
        Inventory.Instance.ReloadAmmo(weaponId, weaponMags, weaponIndex);
        animator.SetTrigger("Reload");
    }

    private void SwitchToMelee()
    {
        rig.weight = 0f;
        isRangedActive = false;
        isMeleeActive = true;
        isUnArmed = false;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive, isMeleeActive, isUnArmed, isReloading); // Notify listeners
    }

    private void SwitchToRanged()
    {
        rig.weight = 1f;
        isRangedActive = true;
        isMeleeActive = false;
        isUnArmed = false;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive, isMeleeActive, isUnArmed, isReloading); // Notify listeners
    }

    private void SwitchToUnarmed()
    {
        rig.weight = 0f;
        isRangedActive = false;
        isMeleeActive = false;
        isUnArmed = true;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive, isMeleeActive, isUnArmed, isReloading); // Notify listeners
    }

    public void Reloading()
    {
        isReloading = true;
        OnWeaponStatusChanged?.Invoke(isRangedActive, isMeleeActive, isUnArmed, isReloading);
    }

    public void NotReloading()
    {
        isReloading = false;
        OnWeaponStatusChanged?.Invoke(isRangedActive, isMeleeActive, isUnArmed, isReloading);
    }

    private void UpdateWeaponStatus()
    {

    }

    private void ResetMeleeAttack()
    {
        isMeleeAttackActive = false;
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
