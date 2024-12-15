using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadHandler : MonoBehaviour
{
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Dead()
    {
        dead = true;
        Invoke("Destroy", 30.0f);
    }

    public void Destroy()
    {
        if (gameObject.transform.parent != null)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
