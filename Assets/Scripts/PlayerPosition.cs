using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    // Reference to the player
    public GameObject playerDot;

    void Update()
    {
        // If the playerDot object exists
        if (playerDot != null)
        {
            // Set this object's position to match the playerDot's position
            transform.position = playerDot.transform.position;
        }
    }
}
