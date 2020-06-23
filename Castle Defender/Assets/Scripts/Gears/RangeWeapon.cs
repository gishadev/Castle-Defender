using UnityEngine;

public class RangeWeapon : Gear
{
    public Transform shotPos;
    [Header("Projectile")]
    [SerializeField] private GameObject projPrefab;

    [SerializeField] private int projDamage = 1;
    [SerializeField] private float projSpeed = 1f;
    [SerializeField] private float projKnockback = 1f;

    public override void Act()
    {
        AudioManager.Instance.Play("Player_Shoot");
        PlayerController.Instance.playerAnimator.SetTrigger("Shoot");

        SpawnProjectile();
    }

    void SpawnProjectile()
    {
        GameObject proj = Instantiate(projPrefab, shotPos.position, shotPos.rotation);

        Projectile projComp = proj.GetComponent<Projectile>();
        projComp.direction = PlayerController.Instance.lookDirection.normalized;

        projComp.damage = projDamage;
        projComp.speed = projSpeed;
        projComp.knockback = projKnockback;
    }
}
