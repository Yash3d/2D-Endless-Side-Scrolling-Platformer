using UnityEngine;

public class Saw : MonoBehaviour
{
    public float animatorSpeed = 1.2f;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        animator.speed = animatorSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Player.instance.TakeDamage(1);
        }
    }
}
