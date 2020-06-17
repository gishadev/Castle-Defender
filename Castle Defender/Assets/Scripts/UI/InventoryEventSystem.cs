using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
public static class InventoryEventSystem
{
    // When player clicks at hotkey (hotbar area) invGear adds to inventory and vice versa
    public static void CheckCursorInput()
    {
        List<RaycastResult> raycastedData = UIRaycast();
        if (raycastedData.Count == 0)
            return;

        RaycastResult raycast;

        raycast = raycastedData.FirstOrDefault(x => x.gameObject.CompareTag("Hotkey"));
        // If clicks at hotkey 
        if (raycast.gameObject != null)
        {
            Hotkey nowHK = raycast.gameObject.GetComponent<Hotkey>();

            InventorySlot emptyInvSlot = UIManager.Instance.inventory.invSlots.FirstOrDefault(x => x.invGear == null);

            // Changing first empty invSlot data taken from nowHK
            if (nowHK.invGear != null)
            {
                emptyInvSlot.invGear = nowHK.invGear;

                //Updating Data
                UIManager.Instance.inventory.UpdateSlotData(emptyInvSlot, nowHK.invGear);
                UIManager.Instance.hotbar.UpdateHotkeyData(nowHK, null);
            }

            InventoryEventSystem.ReplaceGear();

            Debug.Log("Clicked at hotkey");
            return;
        }

        raycast = raycastedData.FirstOrDefault(x => x.gameObject.CompareTag("InvSlot"));
        // If clicks at inventory slot 
        if (raycast.gameObject != null)
        {
            InventorySlot invSlot = raycast.gameObject.GetComponent<InventorySlot>();

            Hotkey emptyHK = UIManager.Instance.hotbar.hotkeys.FirstOrDefault(x => x.invGear == null);

            // Changing first empty hotkey data taken from invSlot
            if (invSlot.invGear != null)
            {
                emptyHK.invGear = invSlot.invGear;

                //Updating Data
                UIManager.Instance.hotbar.UpdateHotkeyData(emptyHK, invSlot.invGear);
                UIManager.Instance.inventory.UpdateSlotData(invSlot, null);
            }

            InventoryEventSystem.ReplaceGear();

            Debug.Log("Clicked at inventory slot");
            return;
        }
    }

    static List<RaycastResult> UIRaycast()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        return raycastResults;
    }

    public static void ReplaceGear()
    {
        PlayerController.Instance.DeleteOldGear();
        UIManager.Instance.miningBounds.ClearBounds();
        if (UIManager.Instance.hotbar.hotkeys[UIManager.Instance.hotbar.selectedKey].invGear != null)
            PlayerController.Instance.SpawnGear(UIManager.Instance.hotbar.hotkeys[UIManager.Instance.hotbar.selectedKey].invGear);
    }

    public static void AddGearToInventory(InventoryGearData _gearData)
    {
        Hotkey emptyHK = UIManager.Instance.hotbar.hotkeys.FirstOrDefault(x => x.invGear == null);
        //Spawning gear at first empty hotkey slot
        if (emptyHK != null)
            UIManager.Instance.hotbar.UpdateHotkeyData(emptyHK, _gearData);

        //Spawning gear at first empty inventory slot
        else
        {
            InventorySlot emptyInvSlot = UIManager.Instance.inventory.invSlots.FirstOrDefault(x => x.invGear == null);
            UIManager.Instance.inventory.UpdateSlotData(emptyInvSlot, _gearData);
        }
    }
}
