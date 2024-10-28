using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    private PlayerControls controls;
    private bool isMeleeActive = true;

    private void Awake() {
        controls = new PlayerControls();
        controls.Character.Fire.performed += ctx => Fire();
    }

    private void Start()
    {
        
    }

    private void Fire()
    {
        if (isMeleeActive)
        {
            Debug.Log("Fire Melee Weapon");
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
