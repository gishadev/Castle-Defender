using UnityEngine;
using UnityEngine.UI;
public class Hotkey : MonoBehaviour
{
    public InventoryGearData invGear;

    [HideInInspector] public Image bg;
    [HideInInspector] public Image gearImg;
    public void UpdateData(InventoryGearData newInvGear)
    {
        bg = GetComponent<Image>();
        gearImg = transform.GetChild(0).GetComponent<Image>();

        invGear = newInvGear;

        if (invGear != null)
        {
            gearImg.enabled = true;
            gearImg.sprite = invGear.inventorySprite;
        }
        else gearImg.enabled = false;
    }
}