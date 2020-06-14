using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public InventoryGear[] inventoryGears = new InventoryGear[9];
    public Transform hotkeysParent;

    Hotkey[] hotkeys = new Hotkey[9];

    public Color selectedColor, unselectedColor;

    int selectedKey = 0;

    private KeyCode[] keyCodes =
        {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9
    };

    void Start()
    {
        for (int i = 0; i < hotkeys.Length; i++)
        {
            hotkeys[i] = new Hotkey(inventoryGears[i], hotkeysParent.GetChild(i).GetComponent<Image>(), hotkeysParent.GetChild(i).GetChild(0).GetComponent<Image>());

            UpdateHotkeyData(hotkeys[i], inventoryGears[i]);
        }

        SelectKey();
    }

    void Update()
    {
        MouseInput();
        KeyboardInput();
    }

    void SelectKey()
    {
        for (int i = 0; i < hotkeys.Length; i++)
        {
            if (i == selectedKey)
                hotkeys[i].bg.color = selectedColor;
            else
                hotkeys[i].bg.color = unselectedColor;

        }

        PlayerController.Instance.DeleteOldGear();
        if (inventoryGears[selectedKey] != null)
            PlayerController.Instance.TakeGear(inventoryGears[selectedKey]);
    }

    void UpdateHotkeyData(Hotkey _hotkey, InventoryGear _gearData)
    {
        _hotkey.UpdateData(_gearData);
    }

    #region Input
    void MouseInput()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            selectedKey--;

            if (selectedKey < 0)
                selectedKey = 8;

            SelectKey();
        }

        else if (Input.mouseScrollDelta.y < 0)
        {
            selectedKey++;

            if (selectedKey > 8)
                selectedKey = 0;

            SelectKey();
        }
    }
    void KeyboardInput()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                selectedKey = i;
                SelectKey();
            }

        }
    }
    #endregion Input
}

//[System.Serializable]
public class Hotkey
{
    public InventoryGear invGear;

    public Image bg;
    public Image gearSprite;

    public Hotkey(InventoryGear _invGear, Image _bg, Image _gearSprite)
    {
        invGear = _invGear;
        bg = _bg;
        gearSprite = _gearSprite;
    }

    public void UpdateData(InventoryGear newInvGear)
    {
        invGear = newInvGear;

        if (invGear != null)
        {
            gearSprite.enabled = true;
            gearSprite.sprite = invGear.inventorySprite;
        }
        else gearSprite.enabled = false;
    }
}