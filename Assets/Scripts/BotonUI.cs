using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonUI : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadScene("Village");
    }
}
