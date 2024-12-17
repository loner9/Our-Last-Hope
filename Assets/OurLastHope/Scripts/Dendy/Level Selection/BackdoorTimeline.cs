using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdoorTimeline : MonoBehaviour {

    public GameObject GameObject1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8)) { 
        GameObject1.SetActive(true);
        
        }
    }
}
