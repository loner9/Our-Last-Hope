using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetector : MonoBehaviour
{
    [SerializeField] GameObject bossHealth;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected with: " + other.gameObject.name);
        if (other.gameObject.tag == "enemy"){
            if (other.gameObject.name == "3D_ZombieBoss"){
                bossHealth.SetActive(true);
            }
        }
    }
}
