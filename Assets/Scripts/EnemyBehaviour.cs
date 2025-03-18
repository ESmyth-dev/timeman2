using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    public enum EnemyState { Idle, Alert }
    public EnemyState state = EnemyState.Idle;

    public bool BehaviourEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        // start coroutine for random raycasting
        StartCoroutine(RandomRaycastRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!BehaviourEnabled || player == null || !agent.isActiveAndEnabled)
        {
            return;
        }
        // do stuff
    }

    IEnumerator RandomRaycastRoutine()
    {
        while (BehaviourEnabled && player != null && agent.isActiveAndEnabled)
        {
            ChooseNewDestination();
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

        int layerMask = LayerMask.GetMask("Level");
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, randomDirection, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

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
}
