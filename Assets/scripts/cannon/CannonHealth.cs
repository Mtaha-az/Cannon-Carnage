using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonHealth : MonoBehaviour
{
    public GameObject explosionPrefab; // Assign your explosion prefab in the Inspector
    public GAMEmanager gameManager; // Reference to the Game Manager
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is tagged as "rock"
        if (collision.gameObject.CompareTag("rock"))
        {
            // Instantiate the explosion at the player's position and rotation
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Call the Game Manager to handle the Game Over logic with a 2.3-second delay
            if (gameManager != null)
            {
                gameManager.TriggerGameOver(2.3f); // Delay of 2.3 seconds
            }
            audioManager.PlaySFX(audioManager.explosion);
            // Destroy the player object after the explosion
            Destroy(gameObject);
        }
    }
}

