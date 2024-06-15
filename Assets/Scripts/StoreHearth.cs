using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreHeart : MonoBehaviour
{
    public Button buyHealthUpgradeButton; 
    public int healthUpgradeCost = 20;  

    private Motor playerMotor; 
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerMotor = player.GetComponent<Motor>();
        }
        // set function to button in panel
        buyHealthUpgradeButton.onClick.AddListener(BuyHealthUpgrade);


    }

    private void Update()
    {
        //set active button if player haa less than 6 hearts and has the money enought 
        if (playerMotor.coinCount >= healthUpgradeCost && playerMotor.currentHealth <= 5)
        {
            buyHealthUpgradeButton.interactable = true;

        }
        else
        {
            buyHealthUpgradeButton.interactable = false;
        }
    }
    void BuyHealthUpgrade()
    {
        if (playerMotor.coinCount >= healthUpgradeCost) // increase health
        {
            playerMotor.coinCount -= 20;
            playerMotor.maxHealth += 1;
            playerMotor.MaxHeal();
        }
        else
        {
            buyHealthUpgradeButton.interactable = false;
        }
    }
}
