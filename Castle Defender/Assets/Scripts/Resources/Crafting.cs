using UnityEngine;
using System.Linq;

public class Crafting : MonoBehaviour
{
    public GameObject uiItemPrefab;
    public GameObject craftingGO;
    [Space]
    public BlueprintData[] blueprintsData;

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
    }

    public void OnCraftBtn(BlueprintData bp_Data)
    {
        // If enough resources => add to inventory
        if (IsEnoughResources(bp_Data.resourcePrices))
            InventoryEventSystem.AddGearToInventory(bp_Data.gearData);
    }

    bool IsEnoughResources(ResourcePrice[] resPrices)
    {
        for (int i = 0; i < resPrices.Length; i++)
        {
           ResourceCount resCount = ResourceManager.Instance.resourcesCount.FirstOrDefault(x => x.resourceData == resPrices[i].resourceData);
            if (resCount.count - resPrices[i].price < 0)
                return false;
            else
                resCount.count -= resPrices[i].price;
        }
        return true;
    }


}