using UnityEngine;

public class FloatingText : MonoBehaviour {

    public TextMesh textMesh;
    public float destroyTime = 1.2f;
    private int randomNumber;

    void Start() {
        randomNumber = Random.Range(1, 101);
        textMesh.text = "+" + randomNumber.ToString();
        Destroy(this.gameObject, destroyTime);
    }
}
