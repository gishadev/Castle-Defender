using UnityEngine;
using System.Collections.Generic;

public class LightsManager : MonoBehaviour
{
    #region Singleton
    public static LightsManager Instance { get; private set; }
    #endregion

    public List<PointLightController> pointLights = new List<PointLightController>();

    Animator globalAnimator;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        globalAnimator = GetComponentInChildren<Animator>();
    }

    public void SetDayLight()
    {
        pointLights.Clear();
        pointLights.AddRange(FindObjectsOfType<PointLightController>());

        foreach (PointLightController plc in pointLights)
        {
            plc.TurnOff();
        }

        globalAnimator.SetBool("isDay", true);
    }

    public void SetNightLight()
    {
        pointLights.Clear();
        pointLights.AddRange(FindObjectsOfType<PointLightController>());

        foreach (PointLightController plc in pointLights)
        {
            plc.TurnOn();
        }

        globalAnimator.SetBool("isDay", false);
    }
}
