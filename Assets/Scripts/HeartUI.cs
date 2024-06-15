using UnityEngine;
using UnityEngine.UI;

public class SpriteDuplicator : MonoBehaviour
{
    public Sprite spriteToDuplicate;
    public Transform imagesContainer;
    private GameObject player;
    private int previousHealth;

    void Start()
    {
        // find player
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            int currentHealth = player.GetComponent<Motor>().currentHealth;
            previousHealth = currentHealth;

            // duplicate heart sprite
            DuplicateSprites(currentHealth);
        }
    }

    void Update()
    {
        if (player != null)
        {
            int currentHealth = player.GetComponent<Motor>().currentHealth;

            // if currentHealth has changed, update the canvas
            if (currentHealth != previousHealth)
            {
                DuplicateSprites(currentHealth);
                previousHealth = currentHealth;
            }
        }
    }

    void DuplicateSprites(int count)
    {
        // delete previous images
        foreach (Transform child in imagesContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject newImageObject = new GameObject("DuplicatedImage_" + (i + 1));
            Image newImage = newImageObject.AddComponent<Image>();
            newImage.sprite = spriteToDuplicate;


            newImageObject.transform.SetParent(imagesContainer);


            RectTransform rectTransform = newImage.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-i * 50, 0); // separation between hearts
            rectTransform.sizeDelta = new Vector2(25, 25); // hearts size
            
        }
    }
}
