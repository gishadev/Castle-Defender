using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [Space]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float smoothness;

    Camera cam;

    float horizontalExtent;
    float posX;
    float vel;
    void Start()
    {
        cam = Camera.main;
        horizontalExtent = cam.orthographicSize * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        float targetX = Mathf.SmoothDamp(posX, target.position.x, ref vel, smoothness, maxSpeed);
        float horizontalExtent = cam.orthographicSize * Screen.width / Screen.height;
        posX = Mathf.Clamp(targetX, WorldBounds.Instance.b_defaultMin.position.x + horizontalExtent, WorldBounds.Instance.b_defaultMax.position.x - horizontalExtent);

        transform.position = new Vector3(posX, transform.position.y, -10f);
    }
}
