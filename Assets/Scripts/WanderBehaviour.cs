using UnityEngine;
using UnityEngine.AI;

public class WanderBehavior : MonoBehaviour
{
    public float wanderRadius = 10f; // Radius within which the agent can wander
    public float wanderTimer = 5f; // Time between position updates

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Update the destination if the timer exceeds the wander timer
        if (timer >= wanderTimer)
        {
            Vector3 newDestination = GetRandomNavMeshPosition(transform.position, wanderRadius);
            if (newDestination != Vector3.zero)
            {
                agent.SetDestination(newDestination);
            }
            timer = 0;
        }
    }

    // Finds a random position within the NavMesh in the given radius
    Vector3 GetRandomNavMeshPosition(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }
}