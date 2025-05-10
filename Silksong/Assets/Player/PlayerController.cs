using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player Data
    public PlayerData playerData;

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
    public float hangCounter;

    //Jumping
    public AudioClip jumpClip;
    public float jumpBufferLength = 0.1f;
    private float jumpBufferCounter;

    //Animation
    public Animator animator;
    public bool facingRight = true;

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
        //terrain Collision Checks
        isGrounded = Physics2D.BoxCast(transform.position, new Vector2(0.79f, 0.5f), 0, -transform.up, 1f, ground);
        isWalled = Physics2D.BoxCast(transform.position, new Vector2(1.6f, 1f), 0, transform.position, 0, ground);

        if (canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");

            //Sprint Bonus
            if (!isWalled)
            {
                if (Input.GetButton("Sprint") && isGrounded)
                {
                    sprintBonus = 2;
                    animator.speed = 2;
                }
                if (Input.GetButtonUp("Sprint"))
                {
                    sprintBonus = 0;
                    animator.speed = 1;
                }
            } 
            else 
            {
                sprintBonus = 0;
                animator.speed = 1;
            }
     
            //Horizontal movement and Sliding
            if (knockBackOn == false) RB.linearVelocity = new Vector2(horizontal * (moveSpeed + sprintBonus), RB.linearVelocity.y);
            else RB.gravityScale = gravity * 10;

            //Hang Time
            if (isGrounded) hangCounter = hangTime;
            else hangCounter -= Time.deltaTime;

            //Jumping
            if (Input.GetButtonDown("Jump")) jumpBufferCounter = jumpBufferLength;
            else jumpBufferCounter -= Time.deltaTime;

            if (jumpBufferCounter >= 0 && hangCounter > 0f)
            {
                SoundManager.instance.PlaySoundClip(jumpClip, transform, 0.25f);
                RB.linearVelocity = new Vector2(RB.linearVelocity.x, jumpForce + sprintBonus / 2);
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
            animator.SetFloat("ySpeed", RB.linearVelocityY);
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

    // Flip the character
    void TurnAround()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canMove = false;
            StartCoroutine(Attacked());
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            canMove = false;
            StartCoroutine(Death());
        }
    }

    //Death
    IEnumerator Death()
    {
        playerData.HP = 0;

        yield return new WaitForSeconds(1);

        transform.position = GameManager.Instance.respawnPoint.position;

        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.name);

        playerData.HP = playerData.maxHP;
        canMove = true;
    }


    //Returns int representing which direction the Player is facing
    public int GetDirection()
    {
        int dir = facingRight ? 1 : -1;
        return dir;
    }

    //Knockback
    IEnumerator Attacked()
    {
        int dir = facingRight ? 1 : -1;
        canMove = false;
        knockBackOn = true;
        RB.linearVelocity = new Vector2(dir * knockBack, knockBack / 2);

        playerData.HP--;

        yield return new WaitForSeconds(0.3f);

        knockBackOn = false;
        canMove = true;

        if(playerData.HP < 1)
        {
            StartCoroutine(Death());
        }
    }
}
