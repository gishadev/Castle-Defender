using UnityEngine;
using UnityEngine.Rendering;

public class Ladder : InteractTarget
{
    public float radiusToInteract;
    [Space]
    public Transform outPosition;

    public string newSortingLayer;

    public bool newCastleBounds;
    public bool isEntrance;

    void Update()
    {
        if (PlayerController.Instance.nowInteractTarget == this && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < radiusToInteract)
        {
            PlayerController.Instance.nowInteractTarget.ShowTargetBounds();

            if (Input.GetKeyDown(KeyCode.E))
                Interact();
        }
    }

    private void Interact()
    {
        PlayerController.Instance.transform.position = outPosition.position;   // Teleporting player on top of castle.

        PlayerController.Instance.GetComponent<SortingGroup>().sortingLayerName = newSortingLayer; // Changing sorting layer to show up player.

        WorldBounds.Instance.castleBounds = newCastleBounds;    // Setting up new castle bounds to change player's area for movement.

        Castle.Instance.UpdateVisibility(isEntrance);
    }

    //private void OnTriggerEnter2D(Collider2D other)   
    //{
    //    PlayerController.Instance.transform.position = outPosition.position;   // Teleporting player on top of castle.

    //    PlayerController.Instance.GetComponent<SortingGroup>().sortingLayerName = newSortingLayer; // Changing sorting layer to show up player.

    //    WorldBounds.Instance.castleBounds = newCastleBounds;    // Setting up new castle bounds to change player's area for movement.

    //    Castle.Instance.UpdateVisibility(isEntrance);
    //}
}
