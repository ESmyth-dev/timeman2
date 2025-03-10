using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;
        right = Vector3.ProjectOnPlane(right, Vector3.up).normalized;

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("movingForwards", true);
            moveDirection += forward;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("movingForwards", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("movingRight", true);
            moveDirection += right;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("movingRight", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("movingLeft", true);
            moveDirection -= right;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("movingLeft", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("movingBackwards", true);
            moveDirection -= forward;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("movingBackwards", false);
        }

        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);
        }
    }
}
