using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMEmanager : MonoBehaviour
{
    public GAMEOVER gameOverPanel; // Reference to your Game Over panel

    // This method will be called to handle the delayed Game Over
    public void TriggerGameOver(float delay)
    {
        StartCoroutine(DelayedGameOver(delay));
    }

    // Coroutine to delay showing the game over panel
    private IEnumerator DelayedGameOver(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Show the Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.Setup();
        }
    }
}
