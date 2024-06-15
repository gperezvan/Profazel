using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    public float speed = 2.0f;
    public float minX = -10.0f; // left limit
    public float maxX = 10.0f;  // right limit
    private float direction = 1.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
    }

    void Update()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            direction = 1.0f; //change direction to right
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            direction = -1.0f; //change direction to left
        }
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            // randomly change direction
            direction = Random.Range(-1.0f, 1.0f);
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }
}
