using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject weaponManager;
    private GameObject[] zombies;
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
        zombies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject zombie in zombies)
        {
            if (zombie.GetComponent<EnemyBehavior>() != null){
                zombie.GetComponent<EnemyBehavior>().enabled = true;
            }else {
                zombie.GetComponent<BossEnemy>().enabled = true;
            }
            zombie.GetComponent<NavMeshAgent>().enabled = true;
            zombie.GetComponent<Animator>().enabled = true;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerAim>().enabled = true;
        player.GetComponentInChildren<Animator>().enabled = true;
        weaponManager.GetComponent<WeaponManager>().enabled = true;
        isGamePaused = false;
    }

    public void Pause(){
        zombies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject zombie in zombies)
        {
            if (zombie.GetComponent<EnemyBehavior>() != null){
                zombie.GetComponent<EnemyBehavior>().enabled = false;
            }else {
                zombie.GetComponent<BossEnemy>().enabled = false;
            }
            zombie.GetComponent<NavMeshAgent>().enabled = false;
            zombie.GetComponent<Animator>().enabled = false;
        }
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerAim>().enabled = false;
        player.GetComponentInChildren<Animator>().enabled = false;
        weaponManager.GetComponent<WeaponManager>().enabled = false;
        isGamePaused = true;
    }
    
}
