using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Debug = UnityEngine.Debug;

public class WeaponVisualController : MonoBehaviour
{
    public static Action<string> OnWeaponChange;
    public static Action<bool> OnReload;
    [SerializeField] private Transform[] weaponTransform;
    [SerializeField] private GameObject mp5;
    [SerializeField] private Transform m161;
    [SerializeField] private Transform sword;
    [SerializeField] private Transform knife;
    private Transform currentWeapon;
    [SerializeField] private Transform leftHand;

    private void Start()
    {
        OnWeaponChange += testWeapon;
        if (mp5 != null)
        {
            Debug.Log("mp5 is not null");
        }else{
            Debug.Log("mp5 is null");
        }
    }

    private void Update()
    {

    }

    private void testWeapon(string s)
    {
        Debug.Log("Weapon : " + s);
        if (s.ToLower().Equals("mp5"))
        {
            // Debug.Log("Switch to mp5, "+ mp5.gameObject.activeSelf);
            SwitchOnWeapons(mp5);
        }
        else if (s.ToLower().Equals("m16"))
        {
            SwitchOnWeapons(m161);
        }
        else if (s.ToLower().Equals("knife"))
        {
            // Debug.Log("Switch to Knife, "+ knife.name);
            SwitchOnWeapons(knife);
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

    private void SwitchOnWeapons(GameObject weapon)
    {

        // currentWeapon = weapon;
        // SwitchOffWeapons();
        weapon.gameObject.SetActive(true);

        // AttachLeftHand();


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
        try
        {
            Transform targetTransform = null;
            targetTransform = currentWeapon.GetComponentInChildren<LeftHandTargetTransform>().transform;
            Transform firePoint = currentWeapon.GetComponentInChildren<FirePointLocator>().transform;
            WeaponManager.OnFirePointChanged(firePoint);
            if (targetTransform != null)
            {
                leftHand.localPosition = targetTransform.localPosition;
                leftHand.localRotation = targetTransform.localRotation;
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Error : " + e);
            throw;
        }
    }
}
