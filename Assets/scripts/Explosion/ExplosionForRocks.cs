using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForRocks : MonoBehaviour
{
    public Sprite[] explosionSprites;  // Array to hold explosion sprites
    public float frameRate = 0.3f;     // Time between each sprite change

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayExplosion());
    }

    IEnumerator PlayExplosion()
    {
        // Loop through all the sprites in the explosion array
        for (int i = 0; i < explosionSprites.Length; i++)
        {
            spriteRenderer.sprite = explosionSprites[i];
            yield return new WaitForSeconds(frameRate);
        }

        // Destroy the explosion object after the animation
        Destroy(gameObject);
    }
}
