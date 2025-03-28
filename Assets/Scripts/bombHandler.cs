using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class bombHandler : MonoBehaviour
{
    bool collided = false;

    public float shrinkDuration = 2f; // Time in seconds to shrink the object
    public Vector3 targetScale = new Vector3(0.4f, 0.4f, 0.4f);
    private Vector3 originalScale;
    Vector3 targetPosition;
    Vector3 originalPosition;

    // Audio
    private AudioClip detonateAudioClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        detonateAudioClip = Resources.Load<AudioClip>("Audio/timebombbubble");

        audioSource.PlayOneShot(detonateAudioClip);
        StartCoroutine(destroySelf(6));
    }

    // Update is called once per frame
    void Update()
    {
        if (collided == false)
        {
            transform.position += transform.forward * Time.deltaTime * 10f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("bullet"))
        {
            transform.parent = collision.transform;
            collided = true;
            GetComponent<Rigidbody>().isKinematic = true;

            
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Stop moving
            collided = true;
            GetComponent<Rigidbody>().isKinematic = true;

            // Get da baddie
            Debug.Log("got da baddie");
            other.GetComponent<Animator>().SetBool("babyMode", true);
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<EnemyBehaviour>().enabled = false;
            other.tag = "downEnemy";
            originalScale = other.transform.localScale;
            originalPosition = new Vector3(other.transform.position.x, other.transform.position.y - 0.9f, other.transform.position.z);
            targetPosition = new Vector3(other.transform.position.x, other.transform.position.y - 0.5f, other.transform.position.z);
            StartCoroutine(ShrinkOverTime(other));

        }
    }

    IEnumerator ShrinkOverTime(Collider other)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            // Interpolate between the original scale and the target scale
            other.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);
            other.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / shrinkDuration);

            elapsedTime += Time.deltaTime; // Increment the elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure the scale is exactly the target scale at the end
        other.transform.localScale = targetScale;
    }

    IEnumerator destroySelf(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

    }


}
