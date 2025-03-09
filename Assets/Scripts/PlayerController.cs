using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("isRunning", true); 
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("isReversing", true);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("isReversing", false);
        }


    }
}
