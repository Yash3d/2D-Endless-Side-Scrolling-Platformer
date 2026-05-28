using Unity.VisualScripting;
using UnityEngine;

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

    private bool isGround;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //isGround = true;
        rb = GetComponent<Rigidbody2D>();
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
    }

    public void jump()
    {
        if (isGround == true)
        {
            rb.linearVelocityY = jumpForce;
            //isGround = false;
        }
    }

    void checkGrounded()
    {
        //Physics2D.OverlapCircle();
    }
        
}
