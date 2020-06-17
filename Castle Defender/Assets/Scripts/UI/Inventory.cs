using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryGO;
    public InventorySlot[] invSlots;

    public void ShowInventory()
    {
        inventoryGO.SetActive(true);
    }

    public void HideInventory()
    {
        inventoryGO.SetActive(false);
    }

    public void UpdateSlotData(InventorySlot _slot, InventoryGearData _gearData)
    {
        _slot.UpdateData(_gearData);
    }
}