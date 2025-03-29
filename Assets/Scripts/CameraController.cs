using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] Transform cameraPivot; // empty object used for rotation
    [SerializeField] Transform playerBody; // the gladitor object (to rotate with horizontal input)
    [SerializeField] Transform cameraTransform; // reference to the actual camera
    [SerializeField] float cameraDistance = 3f; // how far the camera should be behind the player

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);

        playerBody.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        cameraPivot.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);

        Vector3 desiredPosition = cameraPivot.position - cameraPivot.forward * cameraDistance;

        //Raycast from camera pivot outward
        RaycastHit hit;
        LayerMask wallLayers = LayerMask.GetMask("LevelLineOfSight");
        if (Physics.Raycast(cameraPivot.position, (desiredPosition - cameraPivot.position).normalized, out hit, cameraDistance, wallLayers))
        {
            //Move camera to hit point, slightly forward to avoid intersection
            cameraTransform.position = hit.point + hit.normal * 0.2f;
        }
        else
        {
            cameraTransform.position = desiredPosition;
        }

        cameraTransform.rotation = Quaternion.LookRotation(cameraPivot.forward, Vector3.up);
    }
}
