using UnityEngine;

public class ObjectTracking : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.IsObjectDestroyed(gameObject.name))
        {
            Destroy(gameObject);
        }
    }
}
