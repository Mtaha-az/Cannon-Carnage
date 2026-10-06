using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baldesmovement : MonoBehaviour
{
    public bool movePositiveX = true;  // To check whether the object moves in the positive or negative X-axis
    public float speed = 5f;           // Public variable to set speed
    public GameObject explosionPrefab; // Reference to the explosion animation prefab
    AudioManager audioManager;
    public GameObject Diamondprefab;
    public WinCondition winCondition; // Reference to WinCondition script
    public float impulseForce = 15f;

    private bool hasCollidedWithGround = false;  // Tracks if the object has collided with the ground

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        // Find the WinCondition component at runtime
        if (winCondition == null)
        {
            winCondition = FindObjectOfType<WinCondition>();  // Looks for WinCondition script in the scene
        }
    }
    private void Update()
    {
        if (hasCollidedWithGround)  // Only move if the object has collided with the ground
        {
            MoveObject();
        }
    }

    // Function to move the object
    void MoveObject()
    {
        float direction = movePositiveX ? 1 : -1;
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasCollidedWithGround = true;  // Allow the object to start moving
        }

        // Check if the object collided with a wall
        if (collision.gameObject.CompareTag("wall"))
        {
            // Instantiate the explosion at the current object's position before destroying it
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            SpawnDiamonds();
            // Destroy the current game object
            Destroy(gameObject);
            if (winCondition != null)
            {
                winCondition.OnEnemyKilled(); // Notify win condition when a rock is destroyed
            }
        }
    }
    private void SpawnDiamonds()
    {
        int diamondCount = Random.Range(1, 4);  // Random number of diamonds between 1 and 5
        float spawnRadius = 0.5f;  // Adjust the spawn radius as needed
        for (int i = 0; i < diamondCount; i++)
        {
            // Play sound effect for diamonds if you have one
            audioManager.PlaySFX(audioManager.coinsFalling);  // Replace with your diamond sound

            // Randomize the position within the radius for each diamond
            Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0);

            // Instantiate the diamond prefab at the object's position with a random offset
            GameObject diamond = Instantiate(Diamondprefab, transform.position + randomOffset, Quaternion.identity);

            // Apply impulse to the spawned diamond
            Rigidbody2D rb = diamond.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Random upward impulse, with a small horizontal variance
                Vector2 impulseDirection = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
                rb.AddForce(impulseDirection * impulseForce, ForceMode2D.Impulse);
            }
        }
    }

}
