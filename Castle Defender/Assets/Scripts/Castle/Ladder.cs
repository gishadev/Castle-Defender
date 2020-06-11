using UnityEngine;
using UnityEngine.Rendering;

public class Ladder : MonoBehaviour
{
    public Transform outPosition;

    public string newSortingLayer;

    public bool newCastleBounds;
    public bool isEntrance;
    
    private void OnTriggerEnter2D(Collider2D other)   
    {
        PlayerController.Instance.transform.position = outPosition.position;   // Teleporting player on top of castle.

        PlayerController.Instance.GetComponent<SortingGroup>().sortingLayerName = newSortingLayer; // Changing sorting layer to show up player.

        WorldBounds.Instance.castleBounds = newCastleBounds;    // Setting up new castle bounds to change player's area for movement.

        Castle.Instance.UpdateVisibility(isEntrance);
    }
}
