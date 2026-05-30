using UnityEngine;

public class Enemy_Bullet : MonoBehaviour {

    public float bulletSpeed = 2.5f;

    private void Start() {
        Destroy(this.gameObject, 7f);
    }

    void Update() {

        transform.Translate(Vector2.left * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            Player.instance.TakeDamage(1);
            CameraShake.instance.ShakeCamera();
            Destroy(this.gameObject);
        }
    }
}
