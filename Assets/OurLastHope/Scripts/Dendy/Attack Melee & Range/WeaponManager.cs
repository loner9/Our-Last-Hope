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
    [SerializeField] private AudioClip rangedAttackSound; // Suara tembakan
    [SerializeField] private AudioClip meleeAttackSound; // Suara serangan melee
    private AudioSource audioSource; // Sumber audio
    private bool canPlaySound = true;
    private float soundCooldown = 0.2f; // Waktu jeda antara suara
    private float soundTimer = 0f;

    private bool isRangedActive = true;

    // Tambahan untuk efek tembakan
    [SerializeField] private GameObject muzzleFlashPrefab;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Fire.performed += ctx => Fire();
        controls.Character.SwitchToMelee.performed += ctx => SwitchToMelee();
        controls.Character.SwitchToRanged.performed += ctx => SwitchToRanged();

        audioSource = GetComponent<AudioSource>(); // Inisialisasi sumber audio
    }

    private void Start()
    {
        UpdateWeaponStatus();
    }

    private void Update()
    {
        if (!canPlaySound)
        {
            soundTimer += Time.deltaTime;
            if (soundTimer >= soundCooldown)
            {
                canPlaySound = true;
                soundTimer = 0f;
            }
        }
    }

    private void Fire()
    {
        if (canPlaySound)
        {
            if (isRangedActive)
            {
                Debug.Log("Fire Ranged Weapon");
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = firePoint.forward * 20f;
                audioSource.PlayOneShot(rangedAttackSound); // Mainkan suara tembakan

                // Menampilkan dan menghancurkan efek tembakan
                GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
                Destroy(muzzleFlash, 0.2f);
            }
            else
            {
                Debug.Log("Fire Melee Weapon");
                audioSource.PlayOneShot(meleeAttackSound); // Mainkan suara serangan melee
            }
            canPlaySound = false; // Set cooldown untuk suara
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
