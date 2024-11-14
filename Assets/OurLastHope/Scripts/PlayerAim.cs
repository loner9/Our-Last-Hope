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
    [SerializeField] private float minCameraDistance = 1.5f;
    [SerializeField] private float maxCameraDistance = 4f;
    [SerializeField] private float aimSensitivity = 5f;

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
        aim.position = Vector3.Lerp(aim.position, DesiredAimPosition(), aimSensitivity * Time.deltaTime);
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

    private Vector3 DesiredAimPosition()
    {
        Vector3 desiredAimPosition = GetMousePosition();
        Vector3 aimDirection = (desiredAimPosition - transform.position).normalized;

        float distanceToDesiredPosition = Vector3.Distance(transform.position, desiredAimPosition);

        float clamped = Mathf.Clamp(distanceToDesiredPosition, minCameraDistance, maxCameraDistance);
        
        desiredAimPosition = transform.position + aimDirection * clamped;   
        
        desiredAimPosition.y = transform.position.y + 1.5f;

        return desiredAimPosition;
    }
}
