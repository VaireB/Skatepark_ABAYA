using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Vector3 startingPlayerPosition;

    void Start()
    {
        // Store the starting player position when the game starts
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            startingPlayerPosition = player.transform.position;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit an obstacle!");

            // Add your obstacle collision logic here (e.g., decrease health)
            // ...

            // Reset player position to starting position
            RespawnPlayer(collision.gameObject);
        }
    }

    void RespawnPlayer(GameObject player)
    {
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();

        // Set the player's position to the starting position
        playerRigidbody.MovePosition(startingPlayerPosition);

        // Optionally, reset any other player-specific variables or states here
    }
}
