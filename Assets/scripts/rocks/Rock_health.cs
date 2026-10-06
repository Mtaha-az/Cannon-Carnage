using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rock_health : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public TMP_Text healthText;

    public GameObject[] bigRocks;
    public GameObject[] smallRocks;
    public GameObject coinPrefab;

    private bool isGrounded = false;
    private float groundTimer = 0f;// Timer to track how long the rock has been grounded
    private float groundTimeThreshold = 1f;// Time before respawn (1 seconds)
    private RockSpawner rockSpawner; // Reference to RockSpawner for respawning the rock
    private WinCondition winCondition; // Reference to WinCondition script
    private bool check = true;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        health = maxHealth;
        UpdateHealthText();
        rockSpawner = FindObjectOfType<RockSpawner>();
        winCondition = FindObjectOfType<WinCondition>(); // Find WinCondition in the scene
    }

    void Update()
    {
        if (isGrounded)
        {
            groundTimer += Time.deltaTime;
            if (groundTimer >= groundTimeThreshold)
            {
                rockSpawner.RespawnRock(gameObject);
                groundTimer = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioManager.PlaySFXrock(audioManager.Rockcollide);
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            groundTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthText();

        if (health <= 0 && check)
        {
            SpawnSmallRocks();
            SpawnCoins();
            audioManager.PlaySFX(audioManager.brick_break);
            Destroy(gameObject);

            if (winCondition != null)
            {
                winCondition.OnEnemyKilled(); // Notify win condition when a rock is destroyed
            }
            check = false;
        }
    }

    private void SpawnSmallRocks()
    {
        foreach (GameObject bigRock in bigRocks)
        {
            if (gameObject == bigRock)
            {
                // Get a random index for the small rock
                int randomIndex = Random.Range(0, smallRocks.Length);

                // Spawn two small rocks of the same type
                GameObject smallRock1 = Instantiate(smallRocks[randomIndex], transform.position, Quaternion.identity);
                GameObject smallRock2 = Instantiate(smallRocks[randomIndex], transform.position, Quaternion.identity);

                // Add impulse to simulate an explosion effect
                Rigidbody2D rb1 = smallRock1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = smallRock2.GetComponent<Rigidbody2D>();

                if (rb1 != null)
                {
                    Vector2 randomDirection1 = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized; // Random upward direction
                    rb1.AddForce(randomDirection1 * Random.Range(5f, 10f), ForceMode2D.Impulse); // Apply impulse with random strength
                }

                if (rb2 != null)
                {
                    Vector2 randomDirection2 = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized; // Random upward direction
                    rb2.AddForce(randomDirection2 * Random.Range(5f, 10f), ForceMode2D.Impulse); // Apply impulse with random strength
                }

                break; // Exit the loop once the small rocks are spawned
            }
        }
    }


    private void SpawnCoins()
    {
        int coinCount = Random.Range(1, 6);
        float spawnRadius = 0.5f;
        for (int i = 0; i < coinCount; i++)
        {
            audioManager.PlaySFX(audioManager.coinsFalling);
            Vector3 randomOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0);
            Instantiate(coinPrefab, transform.position + randomOffset, Quaternion.identity);
        }
    }

    private void UpdateHealthText()
    {
        healthText.text = health.ToString();
    }
}
