using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject player;
    private Player playerInstance;

    [Header("Score & UI")]
    public float amountToAdd = 1f;
    public Text score_Text;
    public Text highScore_Text;
    private float score = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerInstance = Player.instance;
        highScore_Text.text = PlayerPrefs.GetFloat("HIGHSCORE", 0f).ToString("0");
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(1)) {
            DeleteScore();
        }

        if (player == null || playerInstance.isPlayerDied == true) {
            return;
        }
        else {
            score += amountToAdd * Time.deltaTime;
            score_Text.text = score.ToString("0");
        }

        if (score > PlayerPrefs.GetFloat("HIGHSCORE")) {
            PlayerPrefs.SetFloat("HIGHSCORE", score);
            highScore_Text.text = score.ToString("0");
        }
    }

    void DeleteScore() {
        PlayerPrefs.DeleteKey("HIGHSCORE");
        highScore_Text.text = "0";
    }
}
