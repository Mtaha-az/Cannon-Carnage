using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puasemanu : MonoBehaviour
{
    [SerializeField] GameObject puaseManu;
    public void Puase()
    {
        puaseManu.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(ScaleUp(puaseManu.transform));
    }
    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        StartCoroutine(ScaleDown(puaseManu.transform));
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    // Coroutine for scaling down (when hiding the pop-up)
    IEnumerator ScaleDown(Transform panelTransform)
    {
        Vector3 initialScale = panelTransform.localScale;
        Vector3 finalScale = new Vector3(0, 0, 0);
        float duration = 0.3f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            panelTransform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / duration);
            yield return null;
        }

        puaseManu.SetActive(false);  // Disable the panel after shrinking
    }
}
