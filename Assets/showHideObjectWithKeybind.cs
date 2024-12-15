using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHideObjectWithKeybind : MonoBehaviour {
    public KeyCode KeyCode;
    public GameObject showObject;
    public bool behaviourKeyBind;

    // Update is called once per frame
    void Update() {
        if (behaviourKeyBind == true) {
            if (Input.GetKeyDown(KeyCode)){
                showObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        /*else {
            if (Input.GetKeyDown(KeyCode)) {
                showObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }*/
        
    }
}
