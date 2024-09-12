using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Rotation speed
    public float speed = 100f;
    
    // Direction of rotation: 1 for clockwise, -1 for counterclockwise
    public float rotationDirection = 1;
    
    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(0f, 0f, speed * Time.deltaTime * rotationDirection);
    }
}
