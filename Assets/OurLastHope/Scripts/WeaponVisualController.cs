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
    [SerializeField] private Transform[] weaponTransform;
    [SerializeField] private Transform mp5;
    [SerializeField] private Transform m161;
    [SerializeField] private Transform sword;
    [SerializeField] private Transform knife;
    private Transform currentWeapon;
    [SerializeField] private Transform leftHand;

    void Awake()
    {
        OnWeaponChange += testWeapon;
    }

    private void Update()
    {

    }

    private void testWeapon(string s)
    {
        Debug.Log("Weapon : " + s);
        if (s.ToLower().Equals("mp5"))
        {
            SwitchOnWeapons(mp5);
        }
        else if (s.ToLower().Equals("m16"))
        {
            SwitchOnWeapons(m161);
        }
        else if (s.ToLower().Equals("knife"))
        {
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

    private void SwitchOffWeapons()
    {
        for (int i = 0; i < weaponTransform.Length; i++)
        {
            weaponTransform[i].gameObject.SetActive(false);
        }
    }

    private void AttachLeftHand()
    {

        Transform targetTransform = currentWeapon.GetComponentInChildren<LeftHandTargetTransform>().transform;
        Transform firePoint = currentWeapon.GetComponentInChildren<FirePointLocator>().transform;
        if (firePoint == null){
            Debug.Log("firePoint == null");
        }else{
            Debug.Log("firePoint != null");
        }
        WeaponManager.OnFirePointChanged(firePoint);
        if (targetTransform != null)
        {
            leftHand.localPosition = targetTransform.localPosition;
            leftHand.localRotation = targetTransform.localRotation;
        }

    }

    void OnDisable()
    {
        OnWeaponChange -= testWeapon;
    }
}
