using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinUI : MonoBehaviour
{
    public Text coinText;

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Motor playerController = player.GetComponent<Motor>();
            if (playerController != null)
            {
                coinText.text = playerController.GetCoinCount().ToString();
            }
        }
    }
}
