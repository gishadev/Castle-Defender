using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class PointLightController : MonoBehaviour
{
    private Light2D pointLight;

    void Awake()
    {
        pointLight = GetComponent<Light2D>();
    }

    public void TurnOn()
    {
        pointLight.enabled = true;
    }

    public void TurnOff()
    {
        pointLight.enabled = false;
    }
}
