using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Crafting : MonoBehaviour
{
    public GameObject uiItemPrefab;
    public GameObject craftingGO;
    [Space]
    public BlueprintData[] blueprintsData;

     Dictionary<BlueprintData, GameObject> blueprints = new Dictionary<BlueprintData, GameObject>();
     

    void Start()
    {
        CreateCatalog();
    }

    public void ShowCraftingMenu()
    {
        craftingGO.SetActive(true);
    }

    public void HideCraftingMenu()
    {
        craftingGO.SetActive(false);
    }

    // Creating a list of blueprints from blueprintsData
    void CreateCatalog()
    {
        for (int i = 0; i < blueprintsData.Length; i++)
        {
            CreateUIItem(blueprintsData[i]);
        }
    }
    void CreateUIItem(BlueprintData bp_Data)
    {
        GameObject uiItem = Instantiate(uiItemPrefab, Vector3.zero, Quaternion.identity, craftingGO.transform);
        uiItem.GetComponent<CraftingUIItem>().SetUpItemData(bp_Data);

        blueprints.Add(bp_Data, uiItem);
    }

    void DeleteUIItem(BlueprintData bp_Data)
    {
        Destroy(blueprints[bp_Data]);
        blueprints.Remove(bp_Data);
    }

    public void OnCraftBtn(BlueprintData bp_Data)
    {
        // If enough resources => add to inventory
        if (IsEnoughResources(bp_Data.resourcePrices))
            Craft(bp_Data);
    }

    void Craft(BlueprintData bp_Data)
    {
        SpendResources(bp_Data.resourcePrices);
        DeleteUIItem(bp_Data);

        InventoryEventSystem.AddGearToInventory(bp_Data.gearData);
        InventoryEventSystem.ReplaceGear();
    }

    bool IsEnoughResources(ResourcePrice[] resPrices)
    {
        for (int i = 0; i < resPrices.Length; i++)
        {
            ResourceCount resCount = ResourceManager.Instance.resourcesCount.FirstOrDefault(x => x.resourceData == resPrices[i].resourceData);
            if (resCount.count - resPrices[i].price < 0)
                return false;

        }
        return true;
    }

    void SpendResources(ResourcePrice[] resPrices)
    {
        for (int i = 0; i < resPrices.Length; i++)
        {
            ResourceCount resCount = ResourceManager.Instance.resourcesCount.FirstOrDefault(x => x.resourceData == resPrices[i].resourceData);
            resCount.count -= resPrices[i].price;
        }
    }

}