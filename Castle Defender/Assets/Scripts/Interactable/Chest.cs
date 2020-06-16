using UnityEngine;

public class Chest : InteractTarget
{
    private bool isOpened;

    Animator animator;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //If Player selected chest with a cursor.
        if (UIManager.Instance.nowInteractTarget == this)
            if (!isOpened)
                if (Input.GetKeyDown(KeyCode.E))
                    Open();

        if (isOpened)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InventoryEventSystem.CheckCursorInput();
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Vector2.Distance(PlayerController.Instance.transform.position, transform.position) > radToInteract)
                Close();
        }
    }

    private void Open()
    {
        isOpened = true;

        animator.SetBool("isOpened", true);
        UIManager.Instance.inventory.ShowInventory();
    }

    private void Close()
    {
        isOpened = false;

        animator.SetBool("isOpened", false);
        UIManager.Instance.inventory.HideInventory();
    }
}
