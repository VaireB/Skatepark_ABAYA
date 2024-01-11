// PlayerController.cs
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f; // Added rotation speed
    public float jumpForce = 10f;
    private bool isGrounded;
    private bool hasJumped;

    void Update()
    {
        // Move the player based on input
        MovePlayer();

        // Rotate the player based on input
        RotatePlayer();

        // Jump mechanic
        if (Input.GetButtonDown("Jump") && isGrounded && !hasJumped && IsMoving())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            hasJumped = true;
        }

        // Camera follow
        CameraFollowPlayer();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            hasJumped = false; // Reset jump flag when landing
        }
    }

    void MovePlayer()
    {
        // Get input in world space
        Vector3 moveInput = new Vector3(0f, 0f, Input.GetAxis("Vertical")).normalized;

        // Convert input to world space
        moveInput = Camera.main.transform.TransformDirection(moveInput);
        moveInput.y = 0f; // Ensure no vertical movement

        // Player movement
        transform.Translate(moveInput * moveSpeed * Time.deltaTime, Space.World);
    }

    void RotatePlayer()
    {
        // Get input in world space
        Vector3 rotationInput = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f).normalized;

        // If there is input, rotate the player to face the input direction
        if (rotationInput != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + Mathf.Atan2(rotationInput.x, rotationInput.z) * Mathf.Rad2Deg, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void CameraFollowPlayer()
    {
        // Get the player's forward direction
        Vector3 playerForward = transform.forward;

        // Ignore the vertical component
        playerForward.y = 0f;

        // Normalize to get a unit vector
        playerForward.Normalize();

        // Calculate the new camera position based on the player's forward direction
        Vector3 newCameraPosition = transform.position - playerForward * 5f; // Adjust the multiplier as needed

        // Set the camera position
        Camera.main.transform.position = newCameraPosition;

        // Make the camera look at the player
        Camera.main.transform.LookAt(transform.position + playerForward * 2f); // Adjust the multiplier as needed
    }

    bool IsMoving()
    {
        // Check if there is any movement input
        return Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f;
    }
}
