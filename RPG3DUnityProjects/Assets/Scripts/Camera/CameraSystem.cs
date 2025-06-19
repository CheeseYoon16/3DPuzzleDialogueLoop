using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] float moveSpeed = 50f;
    [SerializeField] float rotateSpeed = 50f;
    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float zoomScale = 5f;
    [SerializeField] float minZoom = 5f;
    [SerializeField] float maxZoom = 15f;
    [SerializeField] CinemachineVirtualCamera cinemachine;

    private bool disabledMovement = false;
    float currentZoom = 50f;

    private void Update()
    {
        if (disabledMovement) return;

        #region CAMERA MOVEMENT
        Vector3 inputDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputDir.z += 1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z -= 1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x -= 1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x += 1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        #endregion

        #region CAMERA ROTATION
        if (Input.GetMouseButton(2))
        {
            // Only read horizontal mouse movement
            float mouseX = Input.GetAxis("Mouse X");

            // Rotate around world-up (Y) axis
            transform.Rotate(Vector3.up, mouseX * rotateSpeed * Time.deltaTime, Space.World);
        }
        #endregion

        #region CAMERA ZOOM
        if(cinemachine != null)
        {
            if(Input.mouseScrollDelta.y > 0)
            {
                currentZoom -= zoomScale;
            }
            
            if(Input.mouseScrollDelta.y < 0)
            {
                currentZoom += zoomScale;
            }

            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            cinemachine.m_Lens.FieldOfView = Mathf.Lerp(cinemachine.m_Lens.FieldOfView, currentZoom, Time.deltaTime * zoomSpeed);
        }
        #endregion
    }
}
