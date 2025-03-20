using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class ShotCollision : MonoBehaviour
{
    private Vector3 direction;
    public int ricochetCount = 1;
    private int ricochets;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;
        ricochets = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Hit();
        }
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().Hit();
        }
        if (ricochets < ricochetCount)
        {
            Vector3 normal = collision.contacts[0].normal;
            direction = Vector3.Reflect(direction, normal); // Reflect the direction
            Debug.Log(direction);
            transform.rotation = Quaternion.LookRotation(direction);
            ricochets++;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
