// CameraFollow.cs
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float distance = 5f; // Adjust this distance as needed

    void LateUpdate()
    {
        // Get the camera's forward direction
        Vector3 cameraForward = transform.forward;

        // Ignore the vertical component
        cameraForward.y = 0f;

        // Normalize to get a unit vector
        cameraForward.Normalize();

        // Calculate the new desired position based on the player's forward direction
        Vector3 desiredPosition = target.position - cameraForward * distance + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the player
        transform.LookAt(target.position);
    }
}
