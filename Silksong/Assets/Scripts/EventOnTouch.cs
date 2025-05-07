using UnityEngine;
using UnityEngine.Events;

public class EventOnTouch : MonoBehaviour
{
    public string newEvent;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.StartEvent(newEvent);
        }
    }
}
