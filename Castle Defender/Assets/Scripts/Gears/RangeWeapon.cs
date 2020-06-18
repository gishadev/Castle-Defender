using UnityEngine;

public class RangeWeapon : Gear
{
    public Transform shotPos;
    [Header("Projectile")]
    public GameObject projPrefab;

    [SerializeField] private int projDamage;
    [SerializeField] private float projSpeed;

    public override void Act()
    {
        SpawnProjectile();
        PlayerController.Instance.playerAnimator.SetTrigger("Shoot");
    }

    void SpawnProjectile()
    {
        GameObject proj = Instantiate(projPrefab, shotPos.position, shotPos.rotation);

        Projectile projComp = proj.GetComponent<Projectile>();
        projComp.speed = projSpeed;
        projComp.damage = projDamage;
    }
}
