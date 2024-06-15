using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    string currentSceneName;
    public GameObject player;
    public GameObject cameraPrincipal;
    public Vector3 posVillage;
    public Vector3 posShop;
    public string scene1;
    public string scene2;

    public Text promptText; 
    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name; 
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraPrincipal = GameObject.FindGameObjectWithTag("MainCamera");

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false); 
        }
    }

    private void Update()
    {
        if (IsPlayerInside())
        {
            promptText.gameObject.SetActive(true); // show W letter
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.W) && IsPlayerInside())
        {
            ChangeScene();
        }
    }

    private bool IsPlayerInside()
    {
        return GetComponent<Collider2D>().OverlapPoint(player.transform.position);
    }

    private void ChangeScene()
    {
        if (currentSceneName == scene1)
        {
            SceneManager.LoadScene(scene2);
            SetPlayerAndCameraPosition(posVillage);
        }
        else if (currentSceneName == scene2)
        {
            SceneManager.LoadScene(scene1);
            SetPlayerAndCameraPosition(posShop);
        }
    }

    private void SetPlayerAndCameraPosition(Vector3 newPosition)
    {
        player.transform.position = newPosition;
 
    }
}
