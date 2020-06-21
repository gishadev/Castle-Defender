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
    public void ChangeResourceValue(ResourceData resourceData, int operation)
    {
        ResourceCount resourceCount = GetResource(resourceData);

        if (resourceCount.count + operation > 0)
            resourceCount.count += operation;
        else
            resourceCount.count = 0;

        UIManager.Instance.UpdateResourcesUIData(resourcesCount);
    }

    public ResourceCount GetResource(ResourceData resource)
    {
        return Array.Find(resourcesCount, x => x.resourceData == resource);
    }
}
