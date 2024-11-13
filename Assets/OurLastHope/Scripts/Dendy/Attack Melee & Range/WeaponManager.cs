using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponManager : MonoBehaviour
{
    public static System.Action<bool, bool, bool> OnWeaponStatusChanged;
    public static Action<string> OnWeaponTypeChanged;
    private PlayerControls controls;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject meleeWeapon;
    [SerializeField] private GameObject rangedWeapon;
    private bool isRangedActive = false;
    private bool isMeleeActive = false;
    private bool isUnArmed = true;
    [SerializeField] private Rig rig;


    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Fire.performed += ctx => Fire();
        // controls.Character.SwitchToMelee.performed += ctx => SwitchToMelee();
        // controls.Character.SwitchToRanged.performed += ctx => SwitchToRanged();
        // controls.Character.Unarmed.performed += ctx => SwitchToUnarmed();
    }

    private void Start()
    {
        rig.weight = 0f;
        OnWeaponTypeChanged += SetWeaponType;
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
        }else{
            SwitchToUnarmed();
        }
    }

    private void Fire()
    {
        if (isRangedActive && !isUnArmed)
        {
            Debug.Log("Fire Ranged Weapon");
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * 20f;
        }
        else if(isMeleeActive && !isUnArmed)
        {
            Debug.Log("Fire Melee Weapon");
        }else{
            Debug.Log("Unarmed");
        }
    }

    private void SwitchToMelee()
    {
        rig.weight = 0f;
        isRangedActive = false;
        isMeleeActive = true;
        isUnArmed = false;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive, isMeleeActive, isUnArmed); // Notify listeners
    }

    private void SwitchToRanged()
    {
        rig.weight = 1f;
        isRangedActive = true;
        isMeleeActive = false;
        isUnArmed = false;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive, isMeleeActive, isUnArmed); // Notify listeners
    }

    private void SwitchToUnarmed()
    {
        rig.weight = 0f;
        isRangedActive = false;
        isMeleeActive = false;
        isUnArmed = true;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive, isMeleeActive, isUnArmed); // Notify listeners
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
