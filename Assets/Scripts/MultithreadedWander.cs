using System.Threading.Tasks;
using UnityEngine;

public class MultithreadedWander : MonoBehaviour
{
    public float wanderRadius = 10f; // Maximum radius for wandering
    public float wanderTimer = 5f; // Time between destination updates
    public float speed = 3f; // Movement speed

    private Vector3 targetPosition; // Current destination
    private bool calculatingDestination = false; // To avoid multiple threads running simultaneously
    private float timer;
    private static System.Random random = new System.Random(); // Thread-safe random generator

    void Start()
    {
        timer = wanderTimer;
        targetPosition = transform.position; // Start at the current position
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Move the agent toward the target position
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Update destination periodically
        if (timer >= wanderTimer && !calculatingDestination)
        {
            timer = 0;
            Vector3 currentPosition = transform.position; // Cache position on the main thread
            CalculateNewDestinationAsync(currentPosition);
        }
    }

    async void CalculateNewDestinationAsync(Vector3 currentPosition)
    {
        calculatingDestination = true;

        // Simulate heavy computation (like pathfinding)
        Vector3 newDestination = await Task.Run(() =>
        {
            // Thread-safe random position
            float randomX = (float)(random.NextDouble() * 2 - 1) * wanderRadius;
            float randomZ = (float)(random.NextDouble() * 2 - 1) * wanderRadius;

            Vector3 randomDirection = new Vector3(randomX, 0, randomZ);
            return currentPosition + randomDirection;
        });

        // Back on the main thread to update the position
        targetPosition = newDestination;
        calculatingDestination = false;
    }
}