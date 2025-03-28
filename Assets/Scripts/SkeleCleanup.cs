using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleCleanup : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, 2f); // Destroy after 2 seconds
    }
}
