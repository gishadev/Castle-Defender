using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Singleton
    public static ResourceManager Instance;
    #endregion

    public Resource[] resources;

    void Awake()
    {
        Instance = this;
    }
}
