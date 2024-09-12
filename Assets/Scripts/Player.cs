using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    // Jump force for the player character
    public float jumpForce = 5f;
    public Rigidbody2D rb;
    
    // Current color of the player
    public string currentColor;
    public SpriteRenderer sr;

    // Predefined color options
    public Color colorCyan;
    public Color colorOrange;
    public Color colorPink;
    public Color colorGreen;

    // Player score and UI elements
    public int playerScore = 0;
    public TextMeshProUGUI scoreText;
    
    // UI objects to display when player dies or wins
    public GameObject playerDeath;
    public GameObject gameOver;
    public GameObject congratsCanvas;

    // Player's dot representation in the game
    public GameObject playerDot;

    // Camera reference
    private Camera mainCamera;

    // Audio clips and audio source for various sound effects
    public AudioClip clickSound;
    public AudioClip defeatSound;
    public AudioClip starSound;
    public AudioClip colorSwitcherSound;
    public AudioSource audioSource;

    void Start ()
    {
        
        // Set a random color for the player and update the score UI
        SetRandomColor(false);
        UpdateScoreUI();

        // Reference to the main camera
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle mouse click for jumping
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
            rb.velocity = Vector2.up * jumpForce;          
        }

        // Check if the player falls off the screen
        float cameraBottomY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        if (transform.position.y < cameraBottomY)
        {
            GameOver();
        }
    }

    // Handle player collisions with different game objects
    void OnTriggerEnter2D (Collider2D other)
    {
        // Handle collision with a ColorSwitcher object
        if (other.tag == "ColorSwitcher")
        {
            PlayColorSwitcherSound();
            SetRandomColor(other.transform.name == "ColorSwitcher");
            Destroy(other.gameObject);
            return;
        }

        // Handle collision with a Star object
        if (other.tag == "Star")
        {
            PlayStarCollectedSound();
            
            // Set the collected star as active in the parent object
            GameObject parentStar = other.gameObject.transform.parent.gameObject;
            Transform starCollected = parentStar.transform.Find("StarCollected");
            if (starCollected != null)
            {
                starCollected.gameObject.SetActive(true);
            } 
            
            Destroy(other.gameObject);
            playerScore += 1;
            UpdateScoreUI();
            return;
        }

        // Check if the player's color does not match the object's color
        if (other.tag != currentColor)
        {
            GameOver();
        }
    }

    // Handle the Game Over scenario
    void GameOver()
    {
        // If the player has collected 8 stars, show the congratulations screen
        if (playerScore == 8)
        {
            PlayStarCollectedSound();
            congratsCanvas.SetActive(true);
            playerDot.SetActive(false);
        }
        else 
        {
            // Otherwise, show the game over screen and play defeat sound
            PlayDefeatSound();
            playerDeath.SetActive(true);
            gameOver.SetActive(true);
            playerDot.SetActive(false);
        }
    }

    // Set a random color for the player
    void SetRandomColor(bool sw)
    {
        // Define a list of colors and their corresponding names
        string[] colorNames = { "Cyan", "Green", "Pink", "Orange" };
        Color[] colors = { colorCyan, colorGreen, colorPink, colorOrange };
        string newColorName;
        Color newColor;
        int newIndex;

        // Ensure we get a different color from the current one and if it's the first colorswitcher of the game, ensure the color doesn't switch to orange
        do
        {
            newIndex = Random.Range(0, colorNames.Length);
            newColorName = colorNames[newIndex];
            newColor = colors[newIndex];
        } while ((currentColor == newColorName) || ((sw) && (newColorName == "Orange")));

        // Update the current color and the SpriteRenderer
        currentColor = newColorName;
        sr.color = newColor;
    }

    // Update the player's score UI
    void UpdateScoreUI()
    {
        scoreText.text = playerScore.ToString();
    }

    // Play the click sound when the player jumps
    void PlayClickSound()
    {
        if (audioSource != null && clickSound != null) 
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    // Play the defeat sound when the game is over
    void PlayDefeatSound()
    {
        if (audioSource != null && defeatSound != null) 
        {
            audioSource.PlayOneShot(defeatSound);
        }
    }

    // Play the sound when a ColorSwitcher is triggered
    void PlayColorSwitcherSound()
    {
        if (audioSource != null && colorSwitcherSound != null) 
        {
            audioSource.PlayOneShot(colorSwitcherSound);
        }
    }

    // Play the sound when a star is collected
    void PlayStarCollectedSound()
    {
        if (audioSource != null && starSound != null) 
        {
            audioSource.PlayOneShot(starSound);
        }
    }
}
