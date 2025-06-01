using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float yawSpeed = 100f;

    public float pitch = 2f;
    float currentZoom = 10f;
    float currentYaw = 0;

    private bool disabledMovement = false;

    private void Update()
    {
        if(disabledMovement) return;

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

    }
    private void LateUpdate()
    {
        if (disabledMovement) return;

        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

    public void SetDisableMovement(bool isEnabled)
    {
        disabledMovement = isEnabled;
    }
}
