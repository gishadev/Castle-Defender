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
    bool isReady;

    ResourceProvider resourceProvider;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        resourceProvider = CheckForResourceProvider();
        // Checking conditions
        if (resourceProvider == null || Vector2.Distance(resourceProvider.transform.position, transform.position) > radius || (int)resourceProvider.canBeGatheredWith != (int)toolType)
        {
            isReady = false;

            UIManager.Instance.miningBounds.ClearBounds();
            return;
        }

        isReady = true;
        UIManager.Instance.miningBounds.CreateBounds(resourceProvider.boxCollider.bounds);
    }

    public override void Act()
    {
        PlayerController.Instance.playerAnimator.SetTrigger("Swing");
        if (isReady)
            resourceProvider.GetHit();
    }

    ResourceProvider CheckForResourceProvider()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(cam.ScreenToWorldPoint(Input.mousePosition), 0.1f);

        if (coll.Length > 0)
        {
            for (int i = 0; i < coll.Length; i++)
            {
                if (coll[i].CompareTag("Resource"))
                    return coll[i].GetComponent<ResourceProvider>();
            }  
        }

        return null;
    }
}
