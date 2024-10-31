using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject weaponManager;
    private bool isGamePaused = false;
    public bool IsGamePaused => isGamePaused;
    private void Awake(){
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume(){
        player.GetComponent<PlayerMovement>().enabled = true;
        weaponManager.GetComponent<WeaponManager>().enabled = true;
        isGamePaused = false;
    }

    public void Pause(){
        player.GetComponent<PlayerMovement>().enabled = false;
        weaponManager.GetComponent<WeaponManager>().enabled = false;
        isGamePaused = true;
    }
}
