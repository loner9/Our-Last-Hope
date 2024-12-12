using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTakenDetector : MonoBehaviour
{
    GameObject[] zombieSpawners;
    [SerializeField] string[] keys;
    // Start is called before the first frame update
    void Start()
    {
        zombieSpawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            if (Inventory.Instance.CheckKeysPresent(keys)){
                Invoke("disableSpawner", 1.5f);
            }
        }
    }

    void disableSpawner(){
        foreach (GameObject spawner in zombieSpawners){
            spawner.SetActive(false);
        }

        Invoke("enableSpawner", 0.5f);
    }

    void enableSpawner(){
        foreach (GameObject spawner in zombieSpawners){
            spawner.SetActive(true);
        }
    }
}
