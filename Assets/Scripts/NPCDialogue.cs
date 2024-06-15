using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public List<string> messages; 

    void Start()
    {
        dialogueText.gameObject.SetActive(false); 
        dialoguePanel.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) //when player enters to collider, show one of the messages list
    {
        if (other.CompareTag("Player"))
        {
            string randomMessage = GetRandomMessage();
            dialogueText.text = randomMessage;
            dialoguePanel.gameObject.SetActive(true);
            dialogueText.gameObject.SetActive(true);
        }
    }
    private string GetRandomMessage()
    {
        if (messages.Count == 0) return ""; // If no messages, return empty string

        int randomIndex = Random.Range(0, messages.Count); 
        return messages[randomIndex];
    }

    private void OnTriggerExit2D(Collider2D other) //hide panel and textwhen player leaves
    {
        if (other.CompareTag("Player"))
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
            if (dialogueText != null)
            {
                dialogueText.gameObject.SetActive(false); 
            }
        }
    }
}
