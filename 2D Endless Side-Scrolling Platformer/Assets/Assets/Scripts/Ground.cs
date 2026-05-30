using UnityEngine;

public class Ground : MonoBehaviour {

    public float destroyTime = 10f;

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            Destroy(this.gameObject, destroyTime);
        }
    }
}
