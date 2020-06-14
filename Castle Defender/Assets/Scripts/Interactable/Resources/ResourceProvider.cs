using UnityEngine;

public class ResourceProvider : InteractTarget
{
    public string targetResourceName;
    //HP 
    public int hitPoints;

    public enum CanBeGatheredWith
    {
        Axe,
        Pickaxe
    }

    public CanBeGatheredWith canBeGatheredWith;

    [Header("Supply Counts")]
    public int minSupplyPerHit;
    public int maxSupplyPerHit;

    public int minFinalSupply;
    public int maxFinalSupply;

    public void GetHit()
    {
        hitPoints--;
        if (hitPoints <= 0)
            DestroyRP();

        ResourceManager.Instance.ChangeResourceValue(targetResourceName, +Random.Range(minSupplyPerHit, maxSupplyPerHit));
    }

    void DestroyRP()
    {
        ResourceManager.Instance.ChangeResourceValue(targetResourceName, +Random.Range(minFinalSupply, maxFinalSupply));
        Destroy(gameObject);
    }
}
