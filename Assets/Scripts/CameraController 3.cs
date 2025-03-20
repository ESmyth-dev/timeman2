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

        // rotate the player and camera horizontally when mouse moves horizontally
        horizontalRotation += mouseX;
        verticalRotation += mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); // limit up/down look angles

        //playerBody.Rotate(Vector3.up * mouseX);
        playerBody.rotation = Quaternion.Euler(0f, horizontalRotation + 180, 0f);
        cameraPivot.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);

        // position the camera behind the pivot object
        Vector3 cameraOffset = new Vector3(0, 0, cameraDistance);

        RaycastHit hit;
        Vector3 targetPosition;
        Vector3 desiredPosition = cameraPivot.position + cameraPivot.rotation * cameraOffset;
        LayerMask wallLayers = LayerMask.GetMask("level");

        if (Physics.Raycast(desiredPosition, playerBody.position - desiredPosition, out hit, (playerBody.position - desiredPosition).magnitude, wallLayers))
        {
            targetPosition = (hit.point - playerBody.position) * 0.8f + playerBody.position;
            /* 
               Note that I move the camera to 80% of the distance
               to the point where an obstruction has been found
               to help keep the sides of the frustrum from still clipping through the wall
            */
        }
        else
        {
            targetPosition = desiredPosition;
        }


        cameraTransform.position = targetPosition;
        cameraTransform.LookAt(cameraPivot);
    }
}
