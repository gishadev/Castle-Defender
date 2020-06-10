using UnityEngine;
using System;
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

    public void ChangeResourceValue(string name, int operation)
    {
        Resource resource = GetResource(name);

        if (resource.count + operation > 0)
            resource.count += operation;
        else
            resource.count = 0;

        UIManager.Instance.UpdateResourcesUIData();
    }

    public Resource GetResource(string name)
    {
        return Array.Find(resources, x => x.resourceName == name);
    }
}
