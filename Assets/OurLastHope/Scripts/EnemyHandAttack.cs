using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    // Start is called before the first frame update
    void Start()
    {
        attackPoint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(){
        attackPoint.gameObject.SetActive(true);
    }

    public void StopAttack(){
        attackPoint.gameObject.SetActive(false);
    }
}
