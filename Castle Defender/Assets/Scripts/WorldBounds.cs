using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    public static WorldBounds Instance { get; private set; }

    public Transform b_Top;
    public Transform b_Bottom;
    public Transform b_Right;
    public Transform b_Left;
    void Awake()
    {
        Instance = this;
    }
}
