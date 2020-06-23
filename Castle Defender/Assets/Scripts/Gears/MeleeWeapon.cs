using UnityEngine;

public class MeleeWeapon : Gear
{
    [SerializeField] private int damage;
    [SerializeField] private float knockback;
    [SerializeField] private float timeToDisableCollider;
    private BoxCollider2D damageCollider;
    void Start()
    {
        damageCollider = GetComponent<BoxCollider2D>();
        DisableDamageCollider();
    }

    void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    public override void Act()
    {
        damageCollider.enabled = true;
        AudioManager.Instance.Play("Player_Punch");
        PlayerController.Instance.playerAnimator.SetTrigger("Punch");
        Invoke("DisableDamageCollider", timeToDisableCollider);
    }


    //Damaging enemies
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IDamageable>().GetDamage(damage);
            other.GetComponent<Rigidbody2D>().AddForce(PlayerController.Instance.lookDirection.normalized * knockback, ForceMode2D.Impulse);
        }

        DisableDamageCollider();
    }
}
