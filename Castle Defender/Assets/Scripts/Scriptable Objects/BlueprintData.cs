using UnityEngine;

[CreateAssetMenu(fileName ="Blueprint",menuName ="Scriptable Objects/Blueprint")]
public class BlueprintData : ScriptableObject
{
    public ResourcePrice[] resourcePrices;
    public InventoryGearData gearData;
}
