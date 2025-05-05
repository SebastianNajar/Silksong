using UnityEngine;

public class Collectible : MonoBehaviour
{
    public PlayerData playerData;
    public int points;
    public bool isBead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerData.score += points;

            Destroy(gameObject);
        }
    }
}
