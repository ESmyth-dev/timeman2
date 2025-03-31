using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSlower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<PlayerController>().timeSlowed)
        {
            GetComponent<ParticleSystem>().playbackSpeed = 0.1f;
        }
        else
        {
            GetComponent<ParticleSystem>().playbackSpeed = 1f;

        }
    }
}
