using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject alive;
    public GameObject skeletonPrefab;
    public GameObject gunObject;
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    public Transform gun;
    public GameObject shotPrefab;
    private float shootTimer = 0f;
    private float shootInterval = 1f; // Time between shots in seconds
    private bool hasSeenPlayer = true; // Set to false if you want enemies to be alerted from start

    public enum EnemyState { Idle, Alert }
    public EnemyState state = EnemyState.Idle;


    public bool BehaviourEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // start coroutine for random raycasting
        StartCoroutine(RandomRaycastRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSeenPlayer)
        {
            hasSeenPlayer = HasLineOfSightToPlayer();
        }

        if (!BehaviourEnabled || player == null || !agent.isActiveAndEnabled || !hasSeenPlayer)
        {
            return;
        }

        // Face player
        Vector3 direction = -(transform.position - player.position).normalized;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * agent.angularSpeed);

        SetWalkingAnimation(direction);


        // Shoot
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f; // Reset the timer
        }


        void Shoot()
        {
            GameObject bullet = Instantiate(shotPrefab, gun.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            bullet.transform.LookAt(player.position + Vector3.up * 1f);

            // set bullet's firer variable
            ShotCollision shotScript = bullet.GetComponent<ShotCollision>();
            shotScript.Firer = gameObject;
        }
    }

    IEnumerator RandomRaycastRoutine()
    {
        while (BehaviourEnabled && player != null && agent.isActiveAndEnabled)
        {
            if (hasSeenPlayer)
            {
                ChooseNewDestination();
            }
            
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    // choose new position on navmesh within line of sight of player
    void ChooseNewDestination()
    {
        // get random direction around player and ignore the veritcal component
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0;

        Vector3 raycastOrigin = player.position + Vector3.up * 1f; // move position up 1 unit so its not on the floor

        Debug.DrawRay(raycastOrigin, randomDirection * 100f, Color.red, 2f);

        int layerMask = LayerMask.GetMask("Level", "LevelLineOfSight");
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, randomDirection, out hit, Mathf.Infinity, layerMask))
        {

            // choose random point along the raycast
            Vector3 randomPointOnLine = Vector3.Lerp(raycastOrigin, hit.point, Random.Range(0f, 1f));

            // find closest navmesh point to selected point
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(randomPointOnLine, out navHit, 2.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(navHit.position);
            }
        }
    }

    private void ResetAnimation()
    {
        animator.SetBool("movingForwards", false);
        animator.SetBool("movingBackwards", false);
        animator.SetBool("movingLeft", false);
        animator.SetBool("movingRight", false);
    }

    private void SetWalkingAnimation(Vector3 direction)
    {
        // Stop animation is speed is near zero
        if (agent.velocity.magnitude < 0.1f)
        {
            ResetAnimation();
            return;
        }

        // Calculate angle between forward direction and movement direction
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

        // Determine which animation to use based on angle
        if (angle > -45 && angle <= 45)
        {
            // forward
            animator.SetBool("movingForwards", true);
            animator.SetBool("movingBackwards", false);
            animator.SetBool("movingLeft", false);
            animator.SetBool("movingRight", false);
        }
        else if (angle > 45 && angle <= 135)
        {
            // right
            animator.SetBool("movingForwards", false);
            animator.SetBool("movingBackwards", false);
            animator.SetBool("movingLeft", false);
            animator.SetBool("movingRight", true);
        }
        else if (angle > -135 && angle <= -45)
        {
            // left
            animator.SetBool("movingForwards", false);
            animator.SetBool("movingBackwards", false);
            animator.SetBool("movingLeft", true);
            animator.SetBool("movingRight", false);
        }
        else
        {
            // backwards
            animator.SetBool("movingForwards", false);
            animator.SetBool("movingBackwards", true);
            animator.SetBool("movingLeft", false);
            animator.SetBool("movingRight", false);
        }
    }

    //When enemy is hit
    public void Hit()
    {
        if (skeletonPrefab)
        {
            GameObject skeleton = Instantiate(skeletonPrefab, transform.position + Vector3.up * 0.5f, transform.rotation);
            skeleton.SetActive(true);
        }
        Destroy(gameObject);
    }

    private bool HasLineOfSightToPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        int layerMask = LayerMask.GetMask("LevelLineOfSight", "Player");

        // Perform a raycast to check for obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }
}
