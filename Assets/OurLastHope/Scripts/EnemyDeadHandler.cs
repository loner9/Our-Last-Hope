using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dead(){
        Invoke("Destroy", 3.0f);
    }

    public void Destroy(){
        Destroy(gameObject);
    }
}
