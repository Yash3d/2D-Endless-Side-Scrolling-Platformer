using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player instance;

    [Header("Player Health")]
    public int maxHealth = 5;
    public Slider playerHealth_Slider;

    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float jumpHeight = 15f;

    public float groundCheckRadius = 0.2f;
    public Transform groundCheckPoint;
    public LayerMask groundCheckLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    private bool isGround;

    [Header("Shooting")]
    public Transform firePoint;
    public float distance = 7f;
    public float rayDuration = 0.13f;
    public LayerMask whatIsEnemy;
    public LineRenderer lr;

    [Header("Floating Text")]
    public GameObject floatingTextPrefab;

    [Header("Camera Shake")]
    public float shakeDuration = 0.12f;
    public float intensity = 3.5f;

    [Header("Effect")]
    public GameObject hitEffectPrefab;
    public GameObject explosionPrefab;
    public Transform explosionSpawnPoint;

    [HideInInspector] public bool isPlayerDied;

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        playerHealth_Slider.maxValue = maxHealth;
        playerHealth_Slider.value = maxHealth;

        isGround = true;
        lr.enabled = false;
        isPlayerDied = false;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        boxCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update() {

        if (transform.position.y <= -2.5f) {
            FallDie();
        }

        moveSpeed += acceleration * Time.deltaTime;
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space)) {
            //Jump();
        }

        CheckIsOnGround();

        if (Input.GetMouseButtonDown(0)) {
            //StartCoroutine(Shoot());
        }
    }

    public void Jump() {

        if (isGround == true) {
            Audio_Manager.instance.PlaySound("Jump");
            rb.linearVelocityY = jumpHeight;
            isGround = false;
            animator.SetBool("Jump", true);
        }
    }

    void CheckIsOnGround() {

        Collider2D collInfo = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundCheckLayer);
        if (collInfo) {
            isGround = true;
        }
    }

    public void Fire() {
        StartCoroutine(Shoot());
    }

     IEnumerator Shoot() {

        Audio_Manager.instance.PlaySound("Shoot");

        lr.enabled = true;
        lr.SetPosition(0, firePoint.position);
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, distance, whatIsEnemy);
        if (hitInfo == true) {
            
            if (hitInfo.transform.gameObject.GetComponent<Enemy_1>() != null) {
                lr.SetPosition(1, hitInfo.point);
                hitInfo.transform.gameObject.GetComponent<Enemy_1>().TakeDamage(1);
                GameObject tempHitEffectPrefab = Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);
                Destroy(tempHitEffectPrefab, 0.65f);
            }
        }
        else {
            lr.SetPosition(1, firePoint.position + transform.right * distance);
        }

        yield return new WaitForSeconds(rayDuration);
        lr.enabled = false;
     }

    public void TakeDamage(int damageAmount) {

        if (maxHealth != 0) {
            maxHealth -= damageAmount;
            animator.SetTrigger("Hurt");
            playerHealth_Slider.value = maxHealth;
            Cinemachine_CameraShake.instance.Shake(shakeDuration, intensity);
            Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        }
        else {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "PlatformerTiles") {
            animator.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box") {
            Ground_Spawner.instance.SpawnGround();
        }

        if (collision.gameObject.tag == "Spear") {
            TakeDamage(1);
        }

        if (collision.gameObject.tag == "Fire_Trap") {
            TakeDamage(1);
        }

        if (collision.gameObject.tag == "Dead_Line") {
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }

    void Die() {
        //Audio_Manager.instance.PlaySound("Explosion");
        playerHealth_Slider.value = 0;
        isPlayerDied = true;
        GameOver.instance.TriggerBackground();
        Debug.Log(this.gameObject.name + " Died.");
        boxCollider2D.enabled = false;
        rb.gravityScale = 0f;
        animator.SetBool("Death", true);
        Cinemachine_CameraShake.instance.Shake(0.15f, 4f);
        Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        this.enabled = false;
    }

    void FallDie() {
        Audio_Manager.instance.PlaySound("Explosion");
        playerHealth_Slider.value = 0;
        isPlayerDied = true;
        GameOver.instance.TriggerBackground();
        Cinemachine_CameraShake.instance.Shake(0.15f, 4f);
        GameObject tempExplosion = Instantiate(explosionPrefab, explosionSpawnPoint.position, Quaternion.identity);
        Destroy(tempExplosion, 0.91f);
        Destroy(this.gameObject);
    }
}
    