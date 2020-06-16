using UnityEngine;
using System;
public class ResourceManager : MonoBehaviour
{
    #region Singleton
    public static ResourceManager Instance;
    #endregion

    public ResourceCount[] resourcesCount;

    void Awake()
    {
        Instance = this;
    }

    public void ChangeResourceValue(string name, int operation)
    {
        ResourceCount resource = GetResource(name);

        if (resource.count + operation > 0)
            resource.count += operation;
        else
            resource.count = 0;

        UIManager.Instance.UpdateResourcesUIData(resourcesCount);
    }

    public ResourceCount GetResource(string name)
    {
        return Array.Find(resourcesCount, x => x.resourceData.m_name == name);
    }
}
