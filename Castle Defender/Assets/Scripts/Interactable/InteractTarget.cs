using UnityEngine;

public class InteractTarget : MonoBehaviour
{
    Bounds interactBounds;

    void Start()
    {
        interactBounds = GetComponent<BoxCollider2D>().bounds;
    }

    public void ShowTargetBounds()
    {
        UIManager.Instance.interactBounds.CreateBounds(interactBounds);
    }

    private void OnMouseEnter()
    {
        PlayerController.Instance.nowInteractTarget = this;
    }

    private void OnMouseExit()
    {
        UIManager.Instance.interactBounds.ClearBounds();
        PlayerController.Instance.nowInteractTarget = null;
    }
}
