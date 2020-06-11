using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    public static WorldBounds Instance { get; private set; }

    public Transform b_defaultMax, b_defaultMin;

    public Transform b_castleMax, b_castleMin;

    [HideInInspector] public bool castleBounds;

    void Awake()
    {
        Instance = this;
    }
}
