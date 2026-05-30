using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public static GameOver instance;
    public GameObject gameOverBackground;

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    void Start() {
        gameOverBackground.transform.localPosition = new Vector3(0f, -1500f, 0f);
    }

    public void TriggerBackground() {
        gameOverBackground.transform.LeanMoveLocalY(0f, 0.8f).setEaseOutBounce();
    }

    public void RetryButton() {
        Audio_Manager.instance.PlaySound("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu() {
        Debug.Log(" Load Menu");
        Audio_Manager.instance.PlaySound("Click");
    }
}
