using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryGearData invGear;

    [HideInInspector] public Image gearImg;

    void Start()
    {
        gearImg = transform.GetChild(0).GetComponent<Image>();
    }

    public void UpdateData(InventoryGearData newInvGear)
    {
        invGear = newInvGear;

        if (invGear != null)
        {
            gearImg.enabled = true;
            gearImg.sprite = invGear.inventorySprite;
        }
        else gearImg.enabled = false;
    }
}
