using UnityEngine;

public class InteractTarget : MonoBehaviour
{
    public float radToInteract = 1f;

    [HideInInspector] public BoxCollider2D coll;

    private void OnMouseOver()
    {
        if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) < radToInteract)
        {
            UIManager.Instance.nowInteractTarget = this;
            UIManager.Instance.ShowInteractable(coll.bounds);
        }
        else
        {
            UIManager.Instance.nowInteractTarget = null;
            UIManager.Instance.HideInteractable();
        }
    }

    private void OnMouseExit()
    {
        UIManager.Instance.nowInteractTarget = null;
        UIManager.Instance.HideInteractable();
    }
}
