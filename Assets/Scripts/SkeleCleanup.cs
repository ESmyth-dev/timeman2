using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleCleanup : MonoBehaviour
{
    private Animator animator;
    AudioSource audioSource;
    public AudioClip[] oldmanAudioClips;
    public AudioClip[] babyAudioClips;

    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag("babySkeleton"))
        {
            babyAudioClips = Resources.LoadAll<AudioClip>("Audio/Baby");
            int randomIndex = Random.Range(0, babyAudioClips.Length);
            audioSource.PlayOneShot(babyAudioClips[randomIndex]);
        }
        else if (gameObject.CompareTag("skeleton"))
        {
            oldmanAudioClips = Resources.LoadAll<AudioClip>("Audio/Oldman");
            int randomIndex = Random.Range(0, oldmanAudioClips.Length);
            audioSource.PlayOneShot(oldmanAudioClips[randomIndex]);
        }

        

        Destroy(gameObject, 2f); // Destroy after 2 seconds
    }
}
