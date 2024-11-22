using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other) {
        Debug.Log("Collision Detected with: " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync("Tutorial Level");
        }
    }
}
