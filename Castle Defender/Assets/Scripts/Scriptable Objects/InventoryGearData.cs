using UnityEngine;

[CreateAssetMenu (fileName = "InventoryItem", menuName = "Scriptable Objects/Inventory Gear")]
public class InventoryGearData : ScriptableObject
{
    public string m_name;
    [Space]
    public GameObject prefab;
    public Sprite inventorySprite;
    public Vector3 spawnPosition;
}
