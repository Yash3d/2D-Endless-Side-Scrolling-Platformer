using UnityEngine;

public class CameraShake : MonoBehaviour {

    public static CameraShake instance;
    private Animator animator;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void ShakeCamera() {
        animator.SetTrigger("Shake");
    }
}
