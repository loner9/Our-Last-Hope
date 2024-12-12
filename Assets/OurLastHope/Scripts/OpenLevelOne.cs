using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevelOne : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        Debug.Log("Level 1 Opened");
        PlayerPrefs.SetInt("Level 1", 1);
    }
}
