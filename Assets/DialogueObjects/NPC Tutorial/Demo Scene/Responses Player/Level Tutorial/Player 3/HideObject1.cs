using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject1 : MonoBehaviour {
    public GameObject obj;

    // Start is called before the first frame update
    void Start() {
        obj.SetActive(false);
    }
}
