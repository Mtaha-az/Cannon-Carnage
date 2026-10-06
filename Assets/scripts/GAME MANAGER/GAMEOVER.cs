using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GAMEOVER : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(ScaleUp(gameObject.transform));
    }
    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Nextlevel()
    {
        SceneManager.LoadScene("level 2");
        Time.timeScale = 1;
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
