using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector2 turn;
    public Vector3 moveDirection;
    public float turnSensitivity = 1.0f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float moveSpeed = 100f;

    public float pitch = 2f;
    float currentZoom = 10f;

    private bool disabledMovement = false;

    private void Update()
    {
        if(disabledMovement) return;

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        if(Input.GetKey(KeyCode.Mouse2))
        {
            Debug.Log("Middle mouse button pressed");
            turn.x += Input.GetAxis("Mouse X") * turnSensitivity;
            turn.y += Input.GetAxis("Mouse Y") * turnSensitivity;
        }

        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        moveDirection = (transform.forward * vertical + transform.right * horizontal);
        moveDirection.y = 0; // keep movement horizontal


    }
    private void LateUpdate()
    {
        if (disabledMovement) return;

        Quaternion rotation = Quaternion.Euler(-turn.y, turn.x, 0);
        Vector3 desiredPosition = target.position - (rotation * offset * currentZoom);

        transform.position = desiredPosition;
    }

    public void SetDisableMovement(bool isEnabled)
    {
        disabledMovement = isEnabled;
    }
}
