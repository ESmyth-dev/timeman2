using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleCleanup : MonoBehaviour
{
    private Animator animator;
    AudioSource audioSource;
    public AudioClip[] oldmanAudioClips;

    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        oldmanAudioClips = Resources.LoadAll<AudioClip>("Audio/Oldman");
        int randomIndex = Random.Range(0, oldmanAudioClips.Length);
        audioSource.PlayOneShot(oldmanAudioClips[randomIndex]);

        Destroy(gameObject, 2f); // Destroy after 2 seconds
    }
}
