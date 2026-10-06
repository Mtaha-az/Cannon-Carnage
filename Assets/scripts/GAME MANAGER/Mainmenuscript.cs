using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenuscript : MonoBehaviour
{
    [SerializeField] GameObject Quitpanel;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject instructionsPanel; // Reference to the instructions panel
    public float instructionDisplayTime = 3.5f;    // Time in seconds to display instructions

    public void Openlevel(int levelId)
    {
        string levelName = "level 1";
        StartCoroutine(ShowInstructionsAndLoadLevel(levelName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitManu()
    {
        Quitpanel.SetActive(true);
        mainPanel.SetActive(false);
        StartCoroutine(ScaleUp(Quitpanel.transform));
    }

    public void Exit_Quitpanal()
    {
        StartCoroutine(ScaleDown(Quitpanel.transform));
    }

    IEnumerator ShowInstructionsAndLoadLevel(string levelName)
    {
        instructionsPanel.SetActive(true); // Show the instructions panel
        StartCoroutine(ScaleUp(instructionsPanel.transform));
        yield return new WaitForSeconds(instructionDisplayTime); // Wait for 3 to 4 seconds
        StartCoroutine(ScaleDown1(instructionsPanel.transform,levelName));
        
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

        Quitpanel.SetActive(false);  // Disable the panel after shrinking
        mainPanel.SetActive(true);
    }
    IEnumerator ScaleDown1(Transform panelTransform, string levelName)
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

        instructionsPanel.SetActive(false); // Hide the instructions panel
        SceneManager.LoadScene(levelName);  // Load the game level
    }
}
