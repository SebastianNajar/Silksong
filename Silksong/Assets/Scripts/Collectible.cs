using UnityEngine;

public class Collectible : MonoBehaviour
{
    public PlayerData playerData;
    public int points;
    public bool isBead;
    public AudioClip collectClip;
    public bool stayDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerData.score += points;

            if (isBead)
            {
                playerData.beads++;
            }

            SoundManager.instance.PlaySoundClip(collectClip, transform, 1);

            if (stayDestroyed)
            {
                GameManager.Instance.RegisterDestroyedObject(gameObject.name);
            }

            Destroy(gameObject);
        }
    }
}
