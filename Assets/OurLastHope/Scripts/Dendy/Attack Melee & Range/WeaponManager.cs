using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private bool isRangedActive = true;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Fire.performed += ctx => Fire();
    }

    private void Start()
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
        }
     
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
