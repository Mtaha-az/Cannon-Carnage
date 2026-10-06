using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    public GameObject cannonballPrefab; // The cannonball prefab
    public float coolDown = 1.5f; // Cooldown between shots
    private float timer;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void ShootCannonball()
    {
        if (timer <= 0)
        {
            audioManager.PlaySFX(audioManager.firing);

            // Create a Quaternion that rotates 90 degrees on the Z-axis
            Quaternion rotation = Quaternion.Euler(0, 0, 90);

            // Instantiate the cannonball with the specified rotation
            Instantiate(cannonballPrefab, transform.position, rotation);

            timer = coolDown; // Reset the timer
        }
    }
}
