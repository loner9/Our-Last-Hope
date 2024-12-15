using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjectTutorial : MonoBehaviour {

    public GameObject obj;
    public GameObject obj2;
    /*public GameObject obj3;
    public GameObject obj4;*/

    public PlayerAim PlayerAim;
    public ApplyRotation ApplyRotation;
    public PlayerMovement PlayerMovement;
    public Animator Animator;

    // Start is called before the first frame update
    void Start() {
        obj.SetActive(true);
        obj2.SetActive(true);
        /*obj3.SetActive(true);
        obj4.SetActive(true);*/

        PlayerAim.isLookAhead = true;
        ApplyRotation.applyBool = true;
        PlayerMovement.enabled = true;
        PlayerAim.enabled = true;
        Animator.enabled = true;
    }

}
