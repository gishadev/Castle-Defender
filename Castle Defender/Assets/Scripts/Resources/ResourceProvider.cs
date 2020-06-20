using UnityEngine;

public class ResourceProvider : MonoBehaviour
{
    public string targetResourceName;
    //HP 
    public int hitPoints;

    [HideInInspector] public BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

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
    [Space]
    public ParticleSystem particles;

    public void GetHit()
    {
        hitPoints--;
        particles.Play();
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
