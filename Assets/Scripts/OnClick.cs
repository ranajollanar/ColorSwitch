using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Reload the current scene (restart the game)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
