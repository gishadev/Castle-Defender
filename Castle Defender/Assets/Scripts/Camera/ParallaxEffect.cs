using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxMultiplier;
    float startX, length;
    Camera cam;

    void Start()
    {
        cam = Camera.main;

        startX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float dist = cam.transform.position.x * parallaxMultiplier;
        float temp = cam.transform.position.x * (1 - parallaxMultiplier);

        float newPosX = startX + dist;

        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

        if (temp > startX + length)
            startX += length;
        else if (temp < startX - length)
            startX -= length;
    }
}
