using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT SOMETHING");
        Destroy(this.gameObject);
    }
}
