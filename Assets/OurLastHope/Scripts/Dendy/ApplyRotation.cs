using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRotation : MonoBehaviour {
    [SerializeField] private float turnSpeed;
    private Player player;

    public bool applyBool;


    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update() {
        
        if (!applyBool)
        {
            ApplyRotation1();
        } else
        {
            ApplyRotation2();
        }
    }

    private void ApplyRotation1()
    {
        //Vector3 lookingDirection = player.aim.GetMousePosition() - transform.position;
        Vector3 lookingDirection = transform.forward;
        lookingDirection.y = 0f;
        lookingDirection.Normalize();

        Quaternion desiredDirection = Quaternion.LookRotation(lookingDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredDirection, turnSpeed * Time.deltaTime);
    }

    private void ApplyRotation2()
    {
        Vector3 lookingDirection = player.aim.GetMousePosition() - transform.position;
        //Vector3 lookingDirection = transform.forward;
        lookingDirection.y = 0f;
        lookingDirection.Normalize();

        Quaternion desiredDirection = Quaternion.LookRotation(lookingDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredDirection, turnSpeed * Time.deltaTime);
    }
}
