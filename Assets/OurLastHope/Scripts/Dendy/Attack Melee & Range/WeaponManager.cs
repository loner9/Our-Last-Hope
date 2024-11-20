using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    public static System.Action<bool, bool, bool, bool> OnWeaponStatusChanged;
    public static Action<string> OnWeaponTypeChanged;
    public static Action<string, int, int> OnWeaponChanged;
    public static Action<int> OnWeaponFired;
    private PlayerControls controls;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject meleeWeapon;
    [SerializeField] private GameObject rangedWeapon;
    private bool isRangedActive = false;
    private bool isMeleeActive = false;
    private bool isUnArmed = true;
    private bool isReloading = false;
    [SerializeField] private Rig rig;
    [SerializeField] private Animator animator;
    private string weaponId;
    private int weaponMags;
    private int weaponIndex;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Fire.performed += ctx =>
        {
            if (isRangedActive && !isReloading)
            {
                Fire();
            }
        };
        controls.Character.Reload.performed += ctx => Reload();
        // controls.Character.SwitchToMelee.performed += ctx => SwitchToMelee();
        // controls.Character.SwitchToRanged.performed += ctx => SwitchToRanged();
        // controls.Character.Unarmed.performed += ctx => SwitchToUnarmed();
    }

    private void Start()
    {
        rig.weight = 0f;
        OnWeaponTypeChanged += SetWeaponType;
        OnWeaponChanged += WeaponDetail;
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

    private void WeaponDetail(string id, int mag, int index)
    {
        weaponId = id;
        weaponMags = mag;
        weaponIndex = index;
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
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = firePoint.forward * 20f;
            }

        }
        else if (isMeleeActive && !isUnArmed)
        {
            Debug.Log("Fire Melee Weapon");
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

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
