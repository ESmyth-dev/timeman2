using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject door;
    void Start()
    {

        if (door == null)
        {
            door = GameObject.Find("Door");
        }

        // compare tag, if tag = start then open door
        if (door.tag == "Start")
        {
            openDoor();
        }



    }

    // Update is called once per frame
    void Update()
    {
        // if enemy count = 0, open doors with tag = end




    }




    void openDoor()
    {
        Debug.Log("Door Opened");
        // lift door slowly up from starting position plus 3 on the y axis to open
        Vector3 targetPosition = door.transform.position + new Vector3(0, 3, 0);
        StartCoroutine(MoveDoor(door.transform, targetPosition, 2.0f));
    }

    private IEnumerator MoveDoor(Transform transform, Vector3 targetPosition, float duration)
    {
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

    void closeDoor()
    {
        Debug.Log("Door Closed");
        // lower door slowly down from current position minus 3 on the y axis to close
        Vector3 targetPosition = door.transform.position - new Vector3(0, 3, 0);
        StartCoroutine(MoveDoor(door.transform, targetPosition, 2.0f));
    }

}

