using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ReloadHandler : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] Rig rig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reload()
    {
        weaponManager.Reloading();
    }

    public void DoneReload(){
        weaponManager.NotReloading();
    }
}
