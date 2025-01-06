using UnityEngine;

public class CullingManager : MonoBehaviour
{
    public Camera targetCamera; // Reference to the Camera
    public string layerToCull = "CullLayer"; // Layer to cull
    public float cullingDistance = 50f; // Distance at which objects on this layer are culled

    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        // Get the layer mask index for the specified layer
        int layerIndex = LayerMask.NameToLayer(layerToCull);

        if (layerIndex == -1)
        {
            Debug.LogError($"Layer '{layerToCull}' does not exist. Please check the layer name.");
            return;
        }

        // Set up culling distances
        float[] layerCullDistances = new float[32]; // Unity has 32 possible layers
        layerCullDistances[layerIndex] = cullingDistance;

        // Apply culling distances to the Camera
        targetCamera.layerCullDistances = layerCullDistances;
        targetCamera.layerCullSpherical = true; // Optional: Enable spherical culling for better accuracy
    }
}