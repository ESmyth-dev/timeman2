using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * 10f;
	
	}

    private void OnCollisionEnter(Collision collision)
    {
		Debug.Log("HIT SOMETHING");
        Destroy(this.gameObject);
    }
}
