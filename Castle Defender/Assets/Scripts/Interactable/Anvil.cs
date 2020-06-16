using UnityEngine;

public class Anvil : InteractTarget
{
    private bool isOpened;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //If Player selected anvil with a cursor.
        if (UIManager.Instance.nowInteractTarget == this)
            if (!isOpened)
                if (Input.GetKeyDown(KeyCode.E))
                    Open();

        if (isOpened)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Vector2.Distance(PlayerController.Instance.transform.position, transform.position) > radToInteract)
                Close();
        }
    }

    private void Open()
    {
        isOpened = true;

        UIManager.Instance.crafting.ShowCraftingMenu();
    }

    private void Close()
    {
        isOpened = false;

        UIManager.Instance.crafting.HideCraftingMenu();
    }
}
