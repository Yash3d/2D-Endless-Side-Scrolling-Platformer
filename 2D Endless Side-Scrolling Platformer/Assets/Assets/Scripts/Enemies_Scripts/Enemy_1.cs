using UnityEngine;

public class Enemy_1 : MonoBehaviour {

    [Header("Enemy Health")]
    public int maxHealth = 5;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    public Vector2 size = new Vector2(8f, 2f);
    public Vector3 offset = new Vector3(-4f, 0f, 0f);
    public LayerMask whatIsPlayer;

    public GameObject enemyBulletPrefab;
    public Transform shootPoint;

    public float shooting_Time = 1.2f;
    private float timeBetweenShoot;
    private bool isEnemyDied;

    [Header("Floating Text")]
    public GameObject floatingTextPrefab;

    void Start() {
        isEnemyDied = false;
        timeBetweenShoot = shooting_Time;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
    }

    void Update() {

        if (isEnemyDied) {
            return;
        }

        Collider2D collInfo = Physics2D.OverlapBox(transform.position + offset, size, 0f, whatIsPlayer);

        if (collInfo) {
            
            if (timeBetweenShoot <= 0f) {
                Shoot();
                timeBetweenShoot = shooting_Time;
            }
            else {
                timeBetweenShoot -= Time.deltaTime;
            }
        }
    }

    void Shoot() {
        Instantiate(enemyBulletPrefab, shootPoint.position, Quaternion.identity);
    }

    public void TakeDamage(int damageAmount) {

        if (maxHealth <= 0) {
            Die();
        }
        else {
            maxHealth -= damageAmount;
            animator.SetTrigger("Hurt");
            Audio_Manager.instance.PlaySound("Damage");
            Cinemachine_CameraShake.instance.Shake(0.13f, 2.5f);
            Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected() {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + offset, size);
    }

    void Die() {
        isEnemyDied = true;
        Debug.Log(this.gameObject.name + " died.");
        animator.SetBool("Death", true);
        rb.gravityScale = 0f;
        boxCollider2D.enabled = false;
        Destroy(this.gameObject, 7f);
        GameObject tempLight = this.gameObject.transform.Find("Light 2D").gameObject;
        tempLight.SetActive(false);
        Cinemachine_CameraShake.instance.Shake(0.13f, 2.5f);
        Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
    }
}
