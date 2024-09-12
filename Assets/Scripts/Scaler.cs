using UnityEngine;

public class Scaler : MonoBehaviour
{
    // Minimum and maximum scale of the object
    public Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f); // Smallest scale
    public Vector3 maxScale = new Vector3(0.2f, 0.2f, 0.2f); // Largest scale
    
    // Speed of the scaling animation
    public float speed = 3f;

    // Boolean to check whether the object is scaling up or down
    private bool scalingUp = true;
    
    void Update()
    {
        // If scaling up, interpolate towards the maximum scale
        if (scalingUp)
        {
            // Smoothly scale towards the maxScale value
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime * speed);
            
            // If the current scale is close to maxScale, start scaling down
            if (Vector3.Distance(transform.localScale, maxScale) < 0.01f)
            {
                scalingUp = false;
            }
        }
        else
        {
            // Smoothly scale towards the minScale value
            transform.localScale = Vector3.Lerp(transform.localScale, minScale, Time.deltaTime * speed);
            
            // If the current scale is close to minScale, start scaling up
            if (Vector3.Distance(transform.localScale, minScale) < 0.01f)
            {
                scalingUp = true;
            }
        }
    }
}
