using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponVisualController : MonoBehaviour
{
    public static Action<string> OnWeaponChange;
    [SerializeField] private Transform[] weaponTransform;
    [SerializeField] private Transform ak12;
    [SerializeField] private Transform m1911;
    [SerializeField] private Transform m161;
    [SerializeField] private Transform aug;
    [SerializeField] private Transform baseball;
    [SerializeField] private Transform sword;

    private Transform currentWeapon;

    [SerializeField] private Transform leftHand;

    private void Start()
    {
        OnWeaponChange += testWeapon;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     SwitchOnWeapons(ak12);
        // }

        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     SwitchOnWeapons(baseball);
        // }

        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {

        // }
    }

    private void testWeapon(string s)
    {   
        if (s.ToLower().Equals("ak12"))
        {
            SwitchOnWeapons(ak12);
        }
        else if (s.ToLower().Equals("m16a1"))
        {
            SwitchOnWeapons(m161);
        }
        else if (s.ToLower().Equals("m1911"))
        {
            SwitchOnWeapons(m1911);
        }
        else if (s.ToLower().Equals("bat"))
        {
            SwitchOnWeapons(baseball);
        }
        else if (s.ToLower().Equals("sword"))
        {
            SwitchOnWeapons(sword);
        }
        else if (s.ToLower().Equals("unarmed"))
        {
            SwitchOffWeapons();
        }
    }

    private void SwitchOnWeapons(Transform weapon)
    {
        currentWeapon = weapon;
        SwitchOffWeapons();
        weapon.gameObject.SetActive(true);

        AttachLeftHand();
    }

    private void SwitchOffWeapons()
    {
        for (int i = 0; i < weaponTransform.Length; i++)
        {
            weaponTransform[i].gameObject.SetActive(false);
        }
    }

    private void AttachLeftHand()
    {
        Transform targetTransform = null;
        targetTransform = currentWeapon.GetComponentInChildren<LeftHandTargetTransform>().transform;
        if (targetTransform != null)
        {
            leftHand.localPosition = targetTransform.localPosition;
            leftHand.localRotation = targetTransform.localRotation;
        }
    }
}
