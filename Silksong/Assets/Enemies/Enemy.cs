using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public int HP;
    private Rigidbody2D RB;
    private int dir = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.linearVelocity = new Vector2(dir * moveSpeed, RB.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            HP--;
            if(HP < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
