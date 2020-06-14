using UnityEngine;
public class MiningTool : Gear
{
    public float radius;

    public enum ToolType
    {
        Axe,
        Pickaxe,
    }

    public ToolType toolType;

    ResourceProvider resourceProvider;
    bool isReady;

    void Update()
    {
        resourceProvider = (ResourceProvider)PlayerController.Instance.nowInteractTarget;
        // Checking conditions
        if (resourceProvider == null || Vector2.Distance(resourceProvider.transform.position, transform.position) > radius || (int)resourceProvider.canBeGatheredWith != (int)toolType)
        {
            isReady = false;

            UIManager.Instance.interactBounds.ClearBounds();
            return;
        }

        isReady = true;
        PlayerController.Instance.nowInteractTarget.ShowTargetBounds();
    }

    public override void Act()
    {
        PlayerController.Instance.playerAnimator.SetTrigger("Swing");
        if (isReady)
            resourceProvider.GetHit();
    }

    //InteractTarget GetInteractTarget()
    //{
    //    Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RaycastHit2D hitInfo = Physics2D.Raycast(cursorPos, Vector3.zero, 0f);

    //    return hitInfo.collider != null && hitInfo.collider.CompareTag("InteractTarget") ? hitInfo.collider.GetComponent<InteractTarget>() : null;
    //}
}
