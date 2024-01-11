using UnityEngine;

public class Ground : MonoBehaviour
{
    // Set the scale factor to adjust the boundary size relative to the ground size
    public float boundaryScaleFactor = 1.0f;

    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Start()
    {
        // Calculate the boundaries based on the ground's size
        CalculateBoundaries();
    }

    void Update()
    {
        // Optionally, you can add boundary checking in the Update method
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Get the player's current position
            Vector3 playerPosition = player.transform.position;

            // Clamp the player's position within the dynamically calculated boundaries
            float clampedX = Mathf.Clamp(playerPosition.x, minBounds.x, maxBounds.x);
            float clampedZ = Mathf.Clamp(playerPosition.z, minBounds.z, maxBounds.z);

            // Set the player's position to the clamped values
            player.transform.position = new Vector3(clampedX, playerPosition.y, clampedZ);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void CalculateBoundaries()
    {
        // Calculate the boundaries based on the ground's size
        Collider groundCollider = GetComponent<Collider>();

        if (groundCollider != null)
        {
            Vector3 groundSize = groundCollider.bounds.size;

            // Calculate the boundaries with the specified scale factor
            minBounds = transform.position - groundSize * 0.5f * boundaryScaleFactor;
            maxBounds = transform.position + groundSize * 0.5f * boundaryScaleFactor;
        }
        else
        {
            Debug.LogError("Ground Collider not found!");
        }
    }
}
