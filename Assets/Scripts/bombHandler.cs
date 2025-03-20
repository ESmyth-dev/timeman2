using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class bombHandler : MonoBehaviour
{
    bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.parent = collision.transform;
        collided = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("got da baddie");
            other.GetComponent<Animator>().SetBool("babyMode", true);
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<EnemyBehaviour>().enabled = false;
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y -0.9f, other.transform.position.z);
        }
    }
}
