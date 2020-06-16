using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    public static WorldBounds Instance { get; private set; }

    public Transform b_Max, b_Min;

    void Awake()
    {
        Instance = this;
    }
}
