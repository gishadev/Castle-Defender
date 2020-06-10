using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public InventoryGear[] inventoryGears = new InventoryGear[9];
    public Image[] hotkeys = new Image[9];

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
                hotkeys[i].color = selectedColor;
            else
                hotkeys[i].color = unselectedColor;

        }

        PlayerController.Instance.DeleteOldGear();
        if (inventoryGears[selectedKey] != null)
            PlayerController.Instance.TakeGear(inventoryGears[selectedKey]);
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
