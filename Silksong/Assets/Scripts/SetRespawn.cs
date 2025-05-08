using UnityEngine;

public class SetRespawn : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.respawnPoint.transform.position = transform.position;
        }
    }
}
