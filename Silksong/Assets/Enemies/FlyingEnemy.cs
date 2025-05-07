using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    //Health
    public int HP;
    public AudioClip deathClip;
    public Animator explosion;

    //Item
    public GameObject item;

    private bool drop;
    public LayerMask player;
    private Rigidbody2D rb;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        drop = Physics2D.BoxCast(transform.position, new Vector2(3f, 10f), 0, -transform.up, 5f, player);

        if (drop)
        {
            rb.gravityScale = 2;
            animator.SetBool("drop", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            HP--;
            if (HP < 1)
            {
                SoundManager.instance.PlaySoundClip(deathClip, transform, 1);
                explosion.transform.position = transform.position;
                explosion.SetTrigger("explode");
                item.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
