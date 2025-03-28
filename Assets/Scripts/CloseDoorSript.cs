using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorSript : MonoBehaviour
{
    // on collision with player, close door
    private GameObject door;
    private bool levelEnded = false;
    public GameObject player;
    // if player collides with current object, close door
    void OnTriggerEnter(Collider other)

    {
        if (other.gameObject.CompareTag("Player") && !levelEnded)

        {
            Debug.Log("Player has entered the finish room");
            // get each game object with tag = finish
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Finish");

            // loop through each door and close it
            foreach (GameObject door in doors)
            {
                // close door

                Vector3 targetPosition = door.transform.position - new Vector3(0, 3, 0);
                StartCoroutine(MoveDoor(door.transform, targetPosition, 2.0f));

            }

        
    }
    IEnumerator MoveDoor(Transform transform, Vector3 targetPosition, float duration)
    {
        levelEnded = true;
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }


        }
    }