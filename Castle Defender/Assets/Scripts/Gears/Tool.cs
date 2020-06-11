using UnityEngine;
public class Tool : Gear
{
    public float radius;

    public enum ToolType
    {
        Axe,
        Pickaxe,

        Repair,
        Build
    }

    public ToolType toolType;

    public override void Act()
    {
        // Checking for tool target
        ToolTarget toolTarget = GetToolTarget();

        PlayerController.Instance.playerAnimator.SetTrigger("Swing");
        if (toolTarget == null || Vector2.Distance(toolTarget.transform.position,transform.position) > radius)
            return;

        switch (toolType)
        {
            case ToolType.Axe:
            case ToolType.Pickaxe:
                ResourceProvider provider = (ResourceProvider)toolTarget; //
                if (provider == null)                                    // Checking if this toolTarget is resourceProvider
                    return;                                             //

                if ((int)provider.canBeGatheredWith == (int)toolType)
                    provider.GetHit();

                break;
            case ToolType.Repair:

                break;
            case ToolType.Build:

                break;
        }
    }

    ToolTarget GetToolTarget()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.Raycast(cursorPos, Vector3.zero, 0f);

        return hitInfo.collider != null && hitInfo.collider.CompareTag("ToolTarget") ? hitInfo.collider.GetComponent<ToolTarget>() : null;
    }
}
