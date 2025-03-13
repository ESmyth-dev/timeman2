using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed = 1.0f;
    public float slowdownFactor = 10;
    public float blinkDistance = 5;
    public float jumpForce;
    public GameObject shotPrefab;
    public Transform gun;
    private bool isGrounded;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animator.applyRootMotion = false;
        rb = GetComponent<Rigidbody>();
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            TeleportAbility();
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

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("jumping", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(shotPrefab, gun.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            go.transform.rotation = transform.rotation;
            GameObject.Destroy(go, 3f);
        }

    }

    public void JumpEnd()
    {
        animator.SetBool("jumping", false);
        animator.SetBool("onGround", false);
    }

    void OnCollisionStay()
    {
        if (!animator.GetBool("jumping"))
        {
            isGrounded = true;
            animator.SetBool("onGround", true);

        }
    }

    IEnumerator SlowTime()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        Time.timeScale *= slowdownFactor;
        speed /= slowdownFactor;
        animator.speed /= slowdownFactor;
    }

    void BlinkAbility()
    {
        Time.timeScale /= slowdownFactor;
        speed *= slowdownFactor;
        animator.speed *= slowdownFactor;
        StartCoroutine(SlowTime());
    }

    void TeleportAbility()
    {
        Vector3 blinkVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) 
        {
            blinkVector += transform.forward;

        }
        if (Input.GetKey(KeyCode.S))
        {
            blinkVector -= transform.forward;

        }
        if (Input.GetKey(KeyCode.D))
        {
            blinkVector += transform.right;

        }
        if (Input.GetKey(KeyCode.A))
        {
            blinkVector -= transform.right;

        }


        if (Physics.Raycast(transform.position + transform.up*0.5f, blinkVector, out RaycastHit hit, blinkDistance, LayerMask.GetMask("Level")))
        {
            Debug.Log("Obstacle detected! shorter teleport");
            transform.position += blinkVector * (hit.distance-1.5f);
        }
        else
        {
            transform.position += blinkVector * blinkDistance;
        }
    }
}

