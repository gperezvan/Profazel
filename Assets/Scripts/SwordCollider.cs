using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Chack if sowrd collider has collide with an enemy 
        if (collision.CompareTag("Enemy"))
        {
            // Call TakeDamage function of enemy Object
            CharacterMovement enemy = collision.GetComponent<CharacterMovement>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }
        if (collision.CompareTag("EnemyFinal")) //check if we're facing with final boss
        {
            EnemyBoss enemy = collision.GetComponent<EnemyBoss>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }
    }
}
