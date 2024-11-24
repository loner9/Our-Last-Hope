using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHandler : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GameObject[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackStart(){
        weaponManager.Attacking();
    }

    public void AttackEnd(){
        weaponManager.NotAttacking();
    }

    public void AttackMelee()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<CapsuleCollider>().enabled = true;
        }
        
    }

    public void StopAttackMelee()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<CapsuleCollider>().enabled = false;
        }
    }


}
