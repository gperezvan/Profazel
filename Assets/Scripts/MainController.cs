using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class Motor : MonoBehaviour
{
    public static Motor instance;
    public float velocidad;
	public float fuerzaSalto;
	public float radioSalto;
	public LayerMask capaPared;
	public bool vivo = true;
	public float horizontal;
    public int coinCount = 0;

    public int maxHealth = 5;
    public int currentHealth;

    private Rigidbody2D miRB;

    public int dificultad;

    public bool hasSword;

    public bool isFacingRight;


    private void Awake()
	{
		miRB = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(gameObject);
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // make object persistent between scenes
            }
            else
            {
                Destroy(gameObject); // destroy duplicates
            }
        }
    }

    void Start()
    {
        hasSword = false;
        currentHealth = maxHealth; 
    }
    void OnTriggerEnter2D(Collider2D other) // collect coins
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinCount += 1;
            other.gameObject.SetActive(false);
        }
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    void Update() //Movement
    {
        if (Input.GetKey(KeyCode.D))
        {
            isFacingRight = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            isFacingRight = false;
        }

        if (!vivo)
		{
			return;
		}
		transform.Translate((Input.GetAxis("Horizontal")) * Time.deltaTime * velocidad * Vector3.right);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Saltar();
		}
    }


	public void Saltar() //jump function
	{
		if (!vivo)
		{
			return;
		}
		Vector2 punto = new Vector2(transform.position.x, transform.position.y);
		Collider2D cols = Physics2D.OverlapCircle(punto, radioSalto, capaPared);
		if (cols != null)
		{
			miRB.velocity = Vector2.zero;
			miRB.AddForce(Vector2.up * fuerzaSalto);
		}
	}

	public void Matar()
	{
        StartCoroutine(EsperarYMatar());
        vivo = false;
        this.MaxHeal(); //heal and respawn in village
        SceneManager.LoadScene("Village");
        vivo = true;
        this.transform.position = new Vector3(0,0,0);
        dificultad = dificultad - 1;
        if (dificultad == 0)
        {
            dificultad = 1;
        }
    }
    public void AumentarDificultad()
    {
        dificultad = dificultad + 1;
        if(dificultad > 5)
        {
            dificultad = 5;
        }
    }
    private IEnumerator EsperarYMatar()
    {
        yield return new WaitForSeconds(5);
    }

    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radioSalto);
	}

    //Life Management
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if(currentHealth == 0)
        {
            this.Matar();
        }
    }

    public void MaxHeal()
    {
        currentHealth = maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void GetSword()
    {
        hasSword = true;
    }

    public int GetDificultad()
    {
        return dificultad;
    }
}
