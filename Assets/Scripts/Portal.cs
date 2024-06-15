using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class Portal : MonoBehaviour
{
    public GameObject espirales;
    public Text wText; 
    string currentSceneName;
    private bool playerInCollider = false;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name; 
        
        if (wText != null){
            wText.gameObject.SetActive(false); //deactivate W letter
        }
    }

    public void ActivarPortal()
    {
        espirales.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInCollider = true;
            if(wText != null)
            {
                wText.gameObject.SetActive(true); // show W letter
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInCollider = false;
            if (wText != null)
            {
                wText.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerInCollider && Input.GetKeyDown(KeyCode.W))
        {
            int record = PlayerPrefs.GetInt("nivel", 1);
            int actual = PlayerPrefs.GetInt("nivelCargado", 1);
            if (actual >= record)
            {
                PlayerPrefs.SetInt("nivel", record + 1);
            }
            GameObject jugador = GameObject.FindWithTag("Player");
            if (currentSceneName == "Village")
            {
                SceneManager.LoadScene("Game");
            }
            else if (currentSceneName == "Game")
            {
                jugador.transform.position = new Vector3(0, 0, 0);
                //if player beats the last level, load the final scene
                if (jugador.GetComponent<Motor>().GetDificultad() == 5)
                {
                    jugador.GetComponent<Motor>().MaxHeal();
                    SceneManager.LoadScene("Final Boss");
                    return;
                }
                //if not, increase the dificulty
                jugador.GetComponent<Motor>().AumentarDificultad();
                jugador.GetComponent<Motor>().MaxHeal();
                SceneManager.LoadScene("Village");
            } else if (currentSceneName == "Final Boss")
            {
                SceneManager.LoadScene("Credits");
                Destroy(jugador);
            }
        }
    }
}
