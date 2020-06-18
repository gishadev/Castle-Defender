using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    public int damage;
    [Space]
    public float moveSpeed;

    [HideInInspector] public Transform targetToFollow;
    [HideInInspector] public Transform targetToAttack;

    [HideInInspector] public Animator animator;
    [Header("Effects")]
    public ParticleSystem e_getDamage;
    void Start()
    {
        TurnEnemy(-1f);
        animator = GetComponent<Animator>();
        moveSpeed += Random.value * 2f;
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
        e_getDamage.Play();

        if (health <= 0)
            Die();
    }

    void Die()
    {
        GameManager.Instance.waves.enemiesCount--;
        Destroy(gameObject);
    }

}
