using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;

    [Range(1, 10)]
    public float moveSpeed;
    private float sprintBonus = 0;

    [Range(1, 20)]
    public float jumpForce;
    [Range(1, 10)]
    public float gravity;

    public Rigidbody2D RB;

    public LayerMask ground;
    public bool isGrounded;
    public bool isWalled;

    //Hang time
    public float hangTime = 0.15f;
    private float hangCounter;

    //Jump buffer
    public float jumpBufferLength = 0.1f;
    private float jumpBufferCounter;

    //Animation
    public Animator animator;
    private bool facingRight = true;

    //Attacked
    public int knockBack = 12;
    private bool knockBackOn = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");

            //terrain Collision Checks
            isGrounded = Physics2D.BoxCast(transform.position, new Vector2(0.79f, 0.5f), 0, -transform.up, 1f, ground);
            isWalled = Physics2D.BoxCast(transform.position, new Vector2(1.6f, 1.5f), 0, transform.position, 0, ground);

            //Sprint Bonus
            if (!isWalled)
            {
                if (Input.GetButton("Sprint") && isGrounded)
                {
                    sprintBonus = 2;
                }
            } 
            else if (isWalled || Input.GetButtonUp("Sprint"))
            {
                sprintBonus = 0;
            }
     
            //Horizontal movement and Sliding
            if (knockBackOn == false) RB.linearVelocity = new Vector2(horizontal * (moveSpeed + sprintBonus), RB.linearVelocity.y);
            else RB.gravityScale = gravity * 10;

            //Hang Time
            if (isGrounded) hangCounter = hangTime;
            else hangCounter -= Time.deltaTime;

            //Jump Buffer
            if (Input.GetButtonDown("Jump")) jumpBufferCounter = jumpBufferLength;
            else jumpBufferCounter -= Time.deltaTime;

            if (jumpBufferCounter >= 0 && hangCounter > 0f)
            {
                RB.linearVelocity = new Vector2(RB.linearVelocity.x, (jumpForce + sprintBonus / 2));
                jumpBufferCounter = 0;
            }
            if (Input.GetButtonUp("Jump") && RB.linearVelocity.y > 0)
            {
                RB.linearVelocity = new Vector2(RB.linearVelocity.x, RB.linearVelocity.y * .5f);
            }
            if (RB.linearVelocity.y < 0 && !isGrounded)
            {
                RB.gravityScale = gravity * 2;
            }
            else
            {
                RB.gravityScale = gravity;
            }

             //Set Animation variables
            animator.SetFloat("xSpeed", Mathf.Abs(RB.linearVelocityX));
            animator.SetFloat("ySpeed", (RB.linearVelocityY));
            animator.SetBool("grounded", isGrounded);

            //Turn Around
            if (horizontal > 0 && !facingRight)
            {
                TurnAround();
            }
            else if (horizontal < 0 && facingRight)
            {
                TurnAround();
            }
        }
    }

    void TurnAround()
    {
        // Flip the character
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canMove = false;
            StartCoroutine(Attacked());
        }
    }

    IEnumerator Attacked()
    {
        int dir = facingRight ? -1 : -1;
        canMove = false;
        knockBackOn = true;
        RB.linearVelocity = new Vector2(dir * knockBack, knockBack / 2);
        animator.SetBool("hurt", true);
        yield return new WaitForSeconds(0.2f);
        knockBackOn = false;
        canMove = true;
        animator.SetBool("hurt", false);
    }
}
