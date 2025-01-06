using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab; // Prefab of the agent to instantiate
    public GameObject plane; // Reference to the plane object
    public int agentCount = 10; // Number of agents to spawn

    void Start()
    {
        SpawnAgents();
    }

    void SpawnAgents()
    {
        // Get the bounds of the plane
        MeshRenderer planeRenderer = plane.GetComponent<MeshRenderer>();
        if (planeRenderer == null)
        {
            Debug.LogError("Plane does not have a MeshRenderer component.");
            return;
        }

        Bounds planeBounds = planeRenderer.bounds;

        // Loop to instantiate agents
        for (int i = 0; i < agentCount; i++)
        {
            // Generate a random position within the plane's bounds
            float x = Random.Range(planeBounds.min.x, planeBounds.max.x);
            float z = Random.Range(planeBounds.min.z, planeBounds.max.z);
            Vector3 spawnPosition = new Vector3(x, plane.transform.position.y, z);

            // Instantiate the agent at the calculated position
            Instantiate(agentPrefab, spawnPosition, Quaternion.identity);
        }
    }
}