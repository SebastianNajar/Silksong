using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Enemy : MonoBehaviour
{
    //Health
    public int HP;
    public AudioClip deathClip;
    public Animator explosion;

    //Item
    public GameObject item;

    //Movement
    public float moveSpeed;
    private int dir = -1;
    private bool facingLeft = true;

    //Physics
    private Rigidbody2D RB;
    public LayerMask ground;
    private BoxCollider2D col;
    private float raySize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        raySize = col.size.x / 1.7f;
        Debug.Log(raySize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(transform.position, -transform.right, raySize, ground) && facingLeft )
        {
            TurnAround();
        }
        else if (Physics2D.Raycast(transform.position, transform.right, raySize, ground) && !facingLeft)
        {
            TurnAround();
        }

        RB.linearVelocity = new Vector2(dir * moveSpeed, RB.linearVelocity.y);
    }

    void TurnAround()
    {
        facingLeft = !facingLeft;
        dir *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            HP--;
            if(HP < 1)
            {
                SoundManager.instance.PlaySoundClip(deathClip, transform, 2);
                explosion.transform.position = transform.position;
                explosion.SetTrigger("explode");
                item.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
