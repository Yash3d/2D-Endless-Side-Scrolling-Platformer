using UnityEngine;
using System.Collections;

public class Enimy : MonoBehaviour
{
    public Vector2 Size = new Vector2(8f, 2f);
    public Vector3 offset = new Vector3(-4f, 0f, 0f);
    public LayerMask whatisPlayer;

    [Header("Attack Settings")]
    public float damage = 20f;
    public float attackRange = 1.5f;  // punch when this close
    public float windUpTime = 0.4f;  // pause before punch lands
    public float cooldownTime = 1.0f;  // pause after punch
    public float moveSpeed = 2.5f;
    public float knockbackForce = 8f;

    [Header("Hit Box")]
    public Transform hitPoint;   // empty child at fist position
    public float hitRadius = 0.6f;

    enum State { Idle, Chase, WindUp, Punch, Cooldown }
    State state = State.Idle;
    bool attackRunning = false;



    Rigidbody2D rb;
    SpriteRenderer sr;
    Transform player;
    bool facingRight = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collinfo = Physics2D.OverlapBox(transform.position + offset, Size, 0f, whatisPlayer);

        if (collinfo)
        {
            Debug.Log("Player is in the range" + collinfo.gameObject.name);

        }

        if (collinfo == null || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        switch (state)
        {
            case State.Idle:
                // Player detected — start chasing
                state = State.Chase;
                break;

            case State.Chase:
                if (dist <= attackRange)
                {
                    // Close enough — start attack
                    if (!attackRunning)
                        StartCoroutine(AttackSequence());
                }
                else
                {
                    // Walk toward player
                    Vector2 dir = (player.position - transform.position).normalized;
                    rb.linearVelocity = new Vector2(dir.x * moveSpeed, rb.linearVelocity.y);
                    FlipToward(dir.x);
                }
                break;
        }

        IEnumerator AttackSequence()
        {
            attackRunning = true;

            // 1. WIND-UP — stop and flash yellow (player can dodge now!)
            state = State.WindUp;
            rb.linearVelocity = Vector2.zero;
            sr.color = Color.yellow;
            yield return new WaitForSeconds(windUpTime);

            // 2. PUNCH — fire the hitbox
            state = State.Punch;
            sr.color = Color.white;
            DoPunchHit();
            yield return new WaitForSeconds(0.15f);

            // 3. COOLDOWN — enemy recovers (punish window!)
            state = State.Cooldown;
            sr.color = Color.gray;
            yield return new WaitForSeconds(cooldownTime);

            // 4. Back to chasing
            sr.color = Color.white;
            state = State.Chase;
            attackRunning = false;
        }

        void DoPunchHit()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                hitPoint.position, hitRadius, whatisPlayer);

            //foreach (var hit in hits)
            //{
            //    //PlayerHealth hp = hit.GetComponent<PlayerHealth>();
            //    if (hp == null) continue;

            //    Vector2 knockDir =
            //        (hit.transform.position - transform.position).normalized;
            //    hp.TakeDamage(damage, knockDir * knockbackForce);
            //}
        }

        void FlipToward(float dirX)
        {
            if (dirX > 0 && !facingRight) { Flip(); }
            if (dirX < 0 && facingRight) { Flip(); }
        }
        void Flip()
        {
            facingRight = !facingRight;
            Vector3 s = transform.localScale;
            s.x *= -1;
            transform.localScale = s;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + offset, Size);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            // Punch hitbox circle
            if (hitPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
            }
        }
    }


}