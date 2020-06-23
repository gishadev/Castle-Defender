using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    // Minimal Wave to appear
    public int minWave;
    [Space]
    public int health;
    public float moveSpeed;

    [HideInInspector] public Transform targetToFollow;
    [HideInInspector] public Transform targetToAttack;

    [HideInInspector] public Animator animator;
    [Header("Effects")]
    public ParticleSystem e_getDamage;
    public GameObject dieEffect;

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

        AudioManager.Instance.Play("Enemy_GetHit");
        e_getDamage.Play();

        if (health <= 0)
            Die();
    }

    void Die()
    {
        AudioManager.Instance.Play("Enemy_Destroy");
        Instantiate(dieEffect, transform.position, Quaternion.identity);

        GameManager.Instance.waves.enemiesCount--;
        Destroy(gameObject);
    }

}
