using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRewind : MonoBehaviour
{
    //The player models position/rotation information
    public Transform playerPosition;

    //List to hold the recorded positions
    public List<Vector3> recordedPositions = new List<Vector3>();

    //List to hold the recorded rotations
    public List<Quaternion> recordedRotations = new List<Quaternion>();

    //When game starts check for player model then start coroutine
    void Start(){
        if(playerPosition == null)
        {
            Debug.LogError("Player Transform is not assigned. Idiot.");
            return;
        }
        StartCoroutine(RecordPositions());
    }

    //This records the players position and rotation, then pauses for 1 sec, and then runs again
    private IEnumerator RecordPositions(){
        while(true)
        {
            //Triggers if positions list is full, and removes the oldest one (also rotation)
            if(recordedPositions.Count < 5){
                recordedPositions.RemoveAt(0);
                recordedRotations.RemoveAt(0);
            }

            //Adds new positions/rotations to their lists
            recordedPositions.Add(playerPosition.position);
            recordedRotations.Add(playerPosition.rotation);

            //Waits 1 sec
            yield return new WaitForSeconds(1f);
        }
    }

    //This will be called when the player dies, and just sets the player to the position/rotation from 5 secs ago
    public void Rewind(){
        //Weird error if there are no positions recorded, shouldn't ever happen
        if(recordedPositions.Count <= 0){
            Debug.LogError("No recorded positions to rewind to. How did you mess this up?");
            return;
        }

        //Sets the player to the position/rotation from 5 secs ago
        playerPosition.position = recordedPositions[0];
        playerPosition.rotation = recordedRotations[0];
    }
}
