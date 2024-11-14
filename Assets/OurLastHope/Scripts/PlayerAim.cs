using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Player player;
    private PlayerControls controls;
    [SerializeField]
    private LayerMask aimLayerMask;
    [SerializeField]
    private Transform aim;
    private Vector3 lookingDirection;

    private Vector2 aimInput;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        AssignInputEvents();
    }

    // Update is called once per frame
    void Update()
    {
        aim.position = new Vector3(GetMousePosition().x, transform.position.y + 1.5f, GetMousePosition().z);
    }

    private void AssignInputEvents()
    {
        controls = player.Controls;

        controls.Character.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Character.Aim.canceled += ctx => aimInput = Vector2.zero;
    }

    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, aimLayerMask)){
            return hit.point;
        }

        return Vector3.zero;
    }
}
