using UnityEngine;
using UnityEngine.UI;

public class Player_Collectibles : MonoBehaviour
{
    public GameObject collectEffectPrefab;

    [Header("Collectables UI")]
    public Text currentGems_Text;
    public Text currentGreenPotion_Text;
    public Slider playerHealth_Slider;

    private int currentGreenPotions;
    private int currentGems;

    private Player player;
    private int maxHealth;

    private void Start()
    {
        currentGems = 0;
        currentGreenPotions = 0;
        player = Player.instance;
        maxHealth = player.maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gems") {
            currentGems += 1;
            currentGems_Text.text = currentGems.ToString();
            GameObject tempCollectEffect = Instantiate(collectEffectPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(tempCollectEffect, 0.45f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Green_Potion") {
            currentGreenPotions += 1;
            currentGreenPotion_Text.text = currentGreenPotions.ToString();
            GameObject tempCollectEffect = Instantiate(collectEffectPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(tempCollectEffect, 0.45f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Heart") {
            if (player.maxHealth < maxHealth) {
                player.maxHealth++;
                playerHealth_Slider.value = player.maxHealth;
                GameObject tempCollectEffect = Instantiate(collectEffectPrefab, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(tempCollectEffect, 0.45f);
                Destroy(collision.gameObject);
            }
        }
    }
}
