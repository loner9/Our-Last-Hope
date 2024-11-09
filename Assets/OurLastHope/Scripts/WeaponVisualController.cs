using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] weaponTransform;
    [SerializeField] private Transform ak12;
    [SerializeField] private Transform m1911;
    [SerializeField] private Transform m161;
    [SerializeField] private Transform aug;
    [SerializeField] private Transform baseball;
    [SerializeField] private Transform sword;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SwitchOnWeapons(ak12);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SwitchOnWeapons(m161);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
        }
    }

    private void SwitchOnWeapons(Transform weapon){
        SwitchOffWeapons();
        weapon.gameObject.SetActive(true);
    }

    private void SwitchOffWeapons()
    {
        for (int i = 0; i < weaponTransform.Length; i++)
        {
            weaponTransform[i].gameObject.SetActive(false);
        }
    }
}
