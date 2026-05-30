using UnityEngine;

public class Camera_Move : MonoBehaviour {

    private Player player;

    private void Start()
    {
        player = Player.instance;
    }

    private void Update()
    {
        if (player.isPlayerDied == true) {
            return;
        }
        transform.Translate(Vector2.right * player.moveSpeed * Time.deltaTime);
    }
}
