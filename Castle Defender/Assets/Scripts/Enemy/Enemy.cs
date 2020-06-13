using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    public int damage;
    [Space]
    public int moveSpeed;

    [HideInInspector] public Transform targetToFollow;
    [HideInInspector] public Transform targetToAttack;

    [HideInInspector] public Animator animator;

    void Start()
    {
        TurnEnemy(-1f);
        animator = GetComponent<Animator>();
    }

    public void TurnEnemy(float x)
    {
        transform.localScale = new Vector2(x, 1f);
    }

    public  virtual void Attack()
    {
    }

    public void GetDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
