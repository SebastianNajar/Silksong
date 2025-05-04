using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Target for the Camera to follow
    public float minX, maxX, minY, maxY; // Bounds for the Camera to stay between

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position;

        Vector3 cameraPosition = new Vector3(targetPosition.x, targetPosition.y, -10);

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minX + camHalfWidth, maxX - camHalfWidth);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minY + camHalfHeight, maxY - camHalfHeight);

        transform.position = cameraPosition;
    }
}
