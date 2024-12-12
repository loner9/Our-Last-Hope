using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class showObjectDialogueBridge : MonoBehaviour {

    public GameObject showObject;
    public int sceneIndex;

    public bool showBool;

    void Update() {
        if (showBool) {
            showObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void ChangeScene() {
        SceneManager.LoadScene(sceneIndex);
    }
   
}
