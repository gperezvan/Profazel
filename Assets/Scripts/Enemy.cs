using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpForce = 300f; 
    public float jumpIntervalMin = 3f; 
    public float jumpIntervalMax = 6f; 
    public float minX = -10.0f; 
    public float maxX = 10.0f;  
    private float direction = 1.0f;
    private Rigidbody2D rb;
    public int maxHealth = 3; 
    private int currentHealth; 
    public CircleCollider2D areaDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
        StartCoroutine(RandomJump());
        currentHealth = maxHealth;
    }

 
    void Update()
    {
        // Move randomly Enemy
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            direction = 1.0f; // change direction right
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            direction = -1.0f; // change direction left
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Motor>().TakeDamage(1);
        }
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            // change randomly direction
            direction = Random.Range(-1.0f, 1.0f);
            yield return new WaitForSeconds(Random.Range(1, 3));
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    IEnumerator RandomJump()
    {
        while (true)
        {
            // jump randomly
            yield return new WaitForSeconds(Random.Range(jumpIntervalMin, jumpIntervalMax));
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("DamageTaken");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
