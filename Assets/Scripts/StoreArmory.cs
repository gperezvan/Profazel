using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreArmory : MonoBehaviour
{
    public Button buySwordButton;  //button to buy sword
    public int swordCost = 20;  

    private Motor playerMotor; 

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerMotor = player.GetComponent<Motor>();
        }

        buySwordButton.onClick.AddListener(GetSword);

        //set if button is selectable
        if (playerMotor.coinCount >= swordCost && !playerMotor.hasSword)
        {
            buySwordButton.interactable = true;
        }
        else
        {
            buySwordButton.interactable = false;
        }
    }

    void GetSword()
    {
        if (playerMotor.coinCount >= swordCost)
        {
            playerMotor.coinCount -= 20;
            playerMotor.GetSword();
            buySwordButton.interactable = false;
        }
        else
        {
            buySwordButton.interactable = false;
        }
    }
}
