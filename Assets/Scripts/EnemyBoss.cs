using System.Collections;
using UnityEngine;

public class EnemyBoss: MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpForce = 600f; 
    public float jumpIntervalMin = 3f; 
    public float jumpIntervalMax = 6f; 
    public float minX = -13.0f;
    public float maxX = 7.0f;  
    private float direction = 1.0f;
    private Rigidbody2D rb;
    public int maxHealth = 3; 
    private int currentHealth; 
    public CircleCollider2D areaDamage;
    public GameObject portal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
        StartCoroutine(RandomJump());
        currentHealth = maxHealth;

        portal.SetActive(false);
    }


    void Update()
    {

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            direction = 1.0f;
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            direction = -1.0f; 
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
            direction = Random.Range(-1.0f, 1.0f);
            yield return new WaitForSeconds(Random.Range(1, 3));
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    IEnumerator RandomJump()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(jumpIntervalMin, jumpIntervalMax));

            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            //Show portal after his death
            Destroy(gameObject);
            portal.SetActive(true);
        }
    }
}
