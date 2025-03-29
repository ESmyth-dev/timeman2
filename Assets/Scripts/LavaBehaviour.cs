using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision){
        Debug.Log("Lava Hit");
        collision.gameObject.GetComponent<PlayerController>().Hit();
    }
}
