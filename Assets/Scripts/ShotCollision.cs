
using UnityEngine;


public class ShotCollision : MonoBehaviour
{
    public GameObject Firer = null;

    private Vector3 direction;
    public int ricochetCount = 1;
    private int ricochets;
    private bool isEnemyBullet = true;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;
        ricochets = 0;
        if (Firer != null && Firer.CompareTag("Player"))
        {
            FindChildWithTag(this.gameObject, "enemyBullet").SetActive(false);
        }
        else
        {
            FindChildWithTag(this.gameObject, "playerBullet").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (Firer != null && Firer.CompareTag("Player"))
        {
            isEnemyBullet = false;
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Hit();
        }
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "downEnemy" && !isEnemyBullet)
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().Hit();
        }
        if (ricochets < ricochetCount && !isEnemyBullet)
        {
            Vector3 normal = collision.contacts[0].normal;
            direction = Vector3.Reflect(direction, normal); // Reflect the direction
            transform.rotation = Quaternion.LookRotation(direction);
            ricochets++;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null; // If no child with the tag was found
    }
}
