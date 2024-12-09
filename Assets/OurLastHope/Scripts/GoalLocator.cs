using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLocator : MonoBehaviour
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("GoalArea");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.transform.position);
        }
    }
}
