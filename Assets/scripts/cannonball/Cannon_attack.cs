using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_attack : MonoBehaviour
{

    public Rigidbody2D cannonballRb;
    public float speed = 2.5f;
    public float range = 1f;
    private float timer;
    public int damage = 10;
    public float knockbackforce = 5;

    // Start is called before the first frame update
    void Start()
    {
        timer = range;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move the cannonball in the Y-axis direction (upward)
        cannonballRb.velocity = Vector2.up * speed;

        // Destroy the cannonball after the set range time
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object hit is a rock
        Rock_health rock = collision.gameObject.GetComponent<Rock_health>();
        if (rock != null)
        {
            // Apply knockback force to the rock
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * knockbackforce, ForceMode2D.Impulse);

            // Deal damage to the rock
            rock.TakeDamage(damage);

            // Destroy the cannonball after impact
            Destroy(gameObject);
        }
    }
}
