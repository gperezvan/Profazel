using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackDuration = 1f;
    public float moveDistance = 1f; 
    public float attackCooldown = 1f; 
    private bool isAttacking = false; 

    private Motor playerMotor;
    private Transform swordSprite;

    void Start()
    {
        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<Motor>();

        // get the sprite of sword
        swordSprite = transform.Find("swordSprite"); 
        if (swordSprite == null)
        {
            Debug.LogError("'swordSprite' not found");
        }
        else
        {
            swordSprite.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // attack if plaer is not attacking and has shord
        if (Input.GetMouseButtonDown(0) && !isAttacking && playerMotor.hasSword)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        swordSprite.gameObject.SetActive(true);
        if (playerMotor.isFacingRight) //right attack
        {
        // Get player's position and direction of attack
        Vector3 playerPosition = playerMotor.transform.position;
        Vector3 initialPosition = playerPosition + Vector3.zero+new Vector3(1,0,0); 

        Vector3 attackDirection = playerMotor.isFacingRight ? transform.right : -transform.right;
        Vector3 attackPosition = playerPosition + attackDirection * moveDistance; 

        swordSprite.localScale = new Vector3(playerMotor.isFacingRight ? 1 : 1, 1, 1);

        // move sword forward
        float elapsedTime = 0;
        while (elapsedTime < attackDuration / 2)
        {
            swordSprite.position = Vector3.Lerp(initialPosition, attackPosition, (elapsedTime / (attackDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move sword backwards
        elapsedTime = 0;
        while (elapsedTime < attackDuration / 2)
        {
            swordSprite.position = Vector3.Lerp(attackPosition, initialPosition, (elapsedTime / (attackDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        }
        else //attack left
        {
            Vector3 playerPosition = playerMotor.transform.position;
            Vector3 initialPosition = playerPosition + Vector3.zero - new Vector3(1, 0, 0); 

            Vector3 attackDirection = playerMotor.isFacingRight ? transform.right : -transform.right;
            Vector3 attackPosition = playerPosition + attackDirection * moveDistance; 

            swordSprite.localScale = new Vector3(playerMotor.isFacingRight ? 1 : 1, -1, 1);

            // Move swprd forward
            float elapsedTime = 0;
            while (elapsedTime < attackDuration / 2)
            {
                swordSprite.position = Vector3.Lerp(initialPosition, attackPosition, (elapsedTime / (attackDuration / 2)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Move sword backwords
            elapsedTime = 0;
            while (elapsedTime < attackDuration / 2)
            {
                swordSprite.position = Vector3.Lerp(attackPosition, initialPosition, (elapsedTime / (attackDuration / 2)));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }


       

        // Hide sword sprite
        swordSprite.gameObject.SetActive(false);
        isAttacking = false;

        // Wait cooldown
        yield return new WaitForSeconds(attackCooldown);
    }


}
