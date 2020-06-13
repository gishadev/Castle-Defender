using UnityEngine;

public class MeleeWeapon : Gear
{
    [SerializeField] private int damage;
    [SerializeField] private float timeToDisableCollider;
    private BoxCollider2D damageCollider;
    private TrailRenderer trailRenderer;

    void Start()
    {
        damageCollider = GetComponent<BoxCollider2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        DisableDamageCollider();
    }

    void DisableDamageCollider()
    {
        damageCollider.enabled = false;
        trailRenderer.enabled = false;
    }

    public override void Act()
    {
        damageCollider.enabled = true;
        trailRenderer.enabled = true;
        PlayerController.Instance.playerAnimator.SetTrigger("Swing");
        Invoke("DisableDamageCollider", timeToDisableCollider);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<IDamageable>().GetDamage(damage);

        DisableDamageCollider();
    }
}
