using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLightScript : MonoBehaviour
{
    public Light flickerLight; // Assign the point light in the Inspector
    public float minInterval = 0.05f; // Minimum flicker interval
    public float maxInterval = 0.3f; // Maximum flicker interval

    void Start()
    {
        if (flickerLight == null)
        {
            flickerLight = GetComponentInChildren<Light>(); // Automatically find child light
        }
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            flickerLight.enabled = Random.value > 0.5f; // 50% chance to toggle
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }
}