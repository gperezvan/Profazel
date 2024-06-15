using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public RectTransform creditsText; 
    public float scrollSpeed = 100f;
    public float startDelay = 2f; 
    public float endDelay = 2f; 
    public string nextSceneName = "Menu"; 

    private float initialPosition; 

    void Start()
    {
        initialPosition = creditsText.anchoredPosition.y;
        StartCoroutine(ScrollCredits());
    }

    IEnumerator ScrollCredits()
    {
        yield return new WaitForSeconds(startDelay); //wait 2 seconds
        while (creditsText.anchoredPosition.y < creditsText.sizeDelta.y)
        {
            creditsText.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(endDelay); //wait 2 seconds
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
    }
}
