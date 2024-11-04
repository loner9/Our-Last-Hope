using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static System.Action<bool> OnWeaponStatusChanged;

    private PlayerControls controls;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject meleeWeapon;
    [SerializeField] private GameObject rangedWeapon;
    

    
    
    


    private bool isRangedActive = true;

    // Tambahan untuk efek tembakan
    [SerializeField] private GameObject muzzleFlashPrefab;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Fire.performed += ctx => Fire();
        controls.Character.SwitchToMelee.performed += ctx => SwitchToMelee();
        controls.Character.SwitchToRanged.performed += ctx => SwitchToRanged();

        
    }

    private void Start()
    {
        UpdateWeaponStatus();
    }

    private void Update()
    {
        
        
            
            
            
                
                
            
        
    }

    private void Fire()
    {
        
        
            if (isRangedActive)
            {
                Debug.Log("Fire Ranged Weapon");
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = firePoint.forward * 20f;
                

                // Menampilkan dan menghancurkan efek tembakan
                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
                Destroy(muzzleFlash, 0.2f);
            }
            else
            {
                Debug.Log("Fire Melee Weapon");
                
            }
            
        
    }

    private void SwitchToMelee()
    {
        isRangedActive = false;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive); // Notify listeners
    }

    private void SwitchToRanged()
    {
        isRangedActive = true;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive); // Notify listeners
    }

    private void UpdateWeaponStatus()
    {
        meleeWeapon.SetActive(!isRangedActive);
        rangedWeapon.SetActive(isRangedActive);
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
