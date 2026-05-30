using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    public void PlayGame() {
        Audio_Manager.instance.PlaySound("Click");
        SceneManager.LoadScene("Level 1");
    }

    public void Menu() {
        Audio_Manager.instance.PlaySound("Click");
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame() {
        Application.Quit();
        Audio_Manager.instance.PlaySound("Click");
        Debug.Log("Exit Game");
    }
}
