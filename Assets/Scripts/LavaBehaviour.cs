using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player"){
            Debug.Log("Lava Hit");

            collider.gameObject.GetComponent<PlayerController>().Hit();
        }
    }
}
