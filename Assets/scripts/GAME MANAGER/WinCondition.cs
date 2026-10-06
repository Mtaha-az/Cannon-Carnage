using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject winPanel;
    public int ttlrock; 
    private int totalRocksSpawned = 0;
    private int rocksKilled = 0;
    private bool spawningComplete = false;

    public void OnSpawningComplete(int totalRocks)
    {
        totalRocksSpawned = totalRocks;
        spawningComplete = true;
        CheckForWin();
    }

    public void OnEnemyKilled()
    {
        rocksKilled++;
        //Debug.Log(rocksKilled);
        CheckForWin();
    }


    void CheckForWin()
    {
        if (spawningComplete && rocksKilled >= ttlrock /*totalRocksSpawned*/)
        {
            StartCoroutine(DisplayWinPanel()); // Start coroutine to handle win panel display
        }
    }

    private IEnumerator DisplayWinPanel()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        winPanel.SetActive(true);
        StartCoroutine(ScaleUp(winPanel.transform));
    }
    IEnumerator ScaleUp(Transform panelTransform)
    {
        Vector3 initialScale = new Vector3(0, 0, 0);
        Vector3 finalScale = new Vector3(1, 1, 1);
        float duration = 0.3f;
        float elapsedTime = 0;

        panelTransform.localScale = initialScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            panelTransform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / duration);
            yield return null;
        }

        panelTransform.localScale = finalScale;
    }
}
