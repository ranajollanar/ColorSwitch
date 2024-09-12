using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Reference to the player's transform (position)
    public Transform player;
    void Update()
    {
        // Check if the player's y-position is higher than the camera's current y-position
        if (player.position.y > transform.position.y)
        {
            // Move the camera's y-position to follow the player while keeping the x and z positions unchanged
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }
}

