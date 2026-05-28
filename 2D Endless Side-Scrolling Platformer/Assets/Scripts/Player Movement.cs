using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    public float MoveSpeed = 5f;
    public float acceleration = 10f;
    public float jumpForce = 10f;
    public float checkRadius = 0.2f;
    public Transform checkground;
    public LayerMask checkgroundlayer;


    private Rigidbody2D rb;
    private Animator animetor;

    private bool isGround;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGround = true;
        rb = GetComponent<Rigidbody2D>();
        animetor = GetComponent<Animator>();
    }

    // Update is called once per frame
    private  void Update()
    {
        MoveSpeed += acceleration * Time.deltaTime;
        transform .Translate(Vector2.right * Time.deltaTime * MoveSpeed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        checkGrounded();
    }

    public void jump()
    {
        if (isGround == true)
        {
            rb.linearVelocityY = jumpForce;
            isGround = false;
            Debug.Log("Jump");
            animetor.SetBool("Jump", true);
        }
    }
    void checkGrounded()
    {
        Collider2D collinfo = Physics2D.OverlapCircle(checkground.position, checkRadius, checkgroundlayer);
        if (collinfo)
        {
            //Debug.Log("player is collider with the ground");
            isGround = true ;
        }
    }

    private void OnDrawGizmosSelected()
    {
       if(checkground == null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(checkground.position, checkRadius);
        }
            
     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        if(collision.gameObject.tag == "Ground")
        {
            animetor.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
   
        
}
