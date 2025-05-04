using UnityEngine;
using static UnityEditor.Progress;

public class Breakable : MonoBehaviour
{
    public int HP;
    public GameObject item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            HP--;
            if (HP < 1)
            {
                item.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
