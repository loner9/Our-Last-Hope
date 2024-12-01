using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    public bool isLookAhead = true;
    CinemachineVirtualCamera virtualCamera;

    [SerializeField] private float minCameraDistance = 1.5f;
    [SerializeField] private float maxCameraDistance = 3f;
    [SerializeField] private float aimCameraSensitivity = 5f;

    private Vector2 aimInput;

    void Awake(){
        GameObject vcam = GameObject.FindGameObjectWithTag("VirtualCam");
        virtualCamera = vcam.GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        AssignInputEvents();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLookAhead)
        {
            aim.position = new Vector3(GetMousePosition().x, transform.position.y + 1.5f, GetMousePosition().z);
            virtualCamera.Follow = this.transform;
        }
        else
        {
            aim.position = Vector3.Lerp(aim.position, DesiredAimPosition(), Time.deltaTime * aimCameraSensitivity);
            virtualCamera.Follow = aim;
        }

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
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, aimLayerMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    Vector3 DesiredAimPosition()
    {
        float actualMaxCamDistance = player.playerMovement.moveInput.y < -.5f ? minCameraDistance : maxCameraDistance;

        Vector3 desiredAimPosition = GetMousePosition();
        Vector3 aimDirection = (desiredAimPosition - transform.position).normalized;

        float distance = Vector3.Distance(transform.position, desiredAimPosition);
        float clampedDistance = Mathf.Clamp(distance, minCameraDistance, actualMaxCamDistance);

        desiredAimPosition = transform.position + aimDirection * clampedDistance;


        desiredAimPosition.y = transform.position.y + 1.35f;

        return desiredAimPosition;
    }
}
