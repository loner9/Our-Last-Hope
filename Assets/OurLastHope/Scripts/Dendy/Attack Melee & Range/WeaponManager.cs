using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static System.Action<bool> OnWeaponStatusChanged;
    public static bool isMeleeAttackActive = false;

    private PlayerControls controls;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject meleeWeapon;
    [SerializeField] private GameObject rangedWeapon;

    private bool isRangedActive = true;

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

            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(muzzleFlash, 0.2f);
        }
        else
        {
            Debug.Log("Fire Melee Weapon");
            isMeleeAttackActive = true;
            // Reset after a brief moment
            Invoke("ResetMeleeAttack", 0.5f);
        }
    }

    private void SwitchToMelee()
    {
        isRangedActive = false;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive);
    }

    private void SwitchToRanged()
    {
        isRangedActive = true;
        UpdateWeaponStatus();
        OnWeaponStatusChanged?.Invoke(isRangedActive);
    }

    private void UpdateWeaponStatus()
    {
        meleeWeapon.SetActive(!isRangedActive);
        rangedWeapon.SetActive(isRangedActive);
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
