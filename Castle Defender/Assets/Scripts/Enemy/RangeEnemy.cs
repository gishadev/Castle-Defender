using UnityEngine;

public class RangeEnemy : Enemy
{
    [Header("Range Variables")]
    public LayerMask followLayer;
    public LayerMask attackLayers;
    [Space]
    public float viewRadius = 3f;
    public float attackRadius = 2f;
    public float stopRadius;
    [Header("Projectile")]
    [SerializeField] private GameObject projPrefab;
    [SerializeField] private int projDamage = 1;
    [SerializeField] private float projSpeed = 1f;

    Vector2 directionToTarget;

    void Update()
    {
        SearchTargetToFollow();

        if (CheckForAttack() && CheckForStop())
        {
            animator.SetBool("isPunching", true);
            return;
        }

        animator.SetBool("isPunching", false);
        //Enemy moves towards target
        float step = moveSpeed * Time.deltaTime;
        if (targetToFollow != null)
            FollowTarget(step);
        else
        {
            transform.Translate(-Vector2.right * step);
            TurnEnemy(-1f);
        }
    }

    #region Detection
    void SearchTargetToFollow()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, viewRadius, followLayer); // If enemy detects player => follow
        if (coll == null)
        {
            targetToFollow = null;
            return;
        }

        targetToFollow = coll.transform;
    }

    bool CheckForAttack()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, attackRadius, attackLayers); // If enemy detects player/castle => attack

        if (coll != null)
        {
            directionToTarget = coll.transform.position - transform.position;

            targetToAttack = coll.transform;
            return true;
        }
        return false;
    }

    bool CheckForStop()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, stopRadius, attackLayers); // If enemy detects target => stop
        if (coll != null)
            return true;

        return false;
    }
    #endregion

    void FollowTarget(float step)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetToFollow.position, step);

        Vector2 direction = targetToFollow.position - transform.position;

        if (direction.x > 0)
            TurnEnemy(1f);
        else if (direction.x < 0)
            TurnEnemy(-1f);
    }

    public override void Attack()
    {
        if (targetToAttack != null)
        {
            AudioManager.Instance.Play("Enemy_Shoot");
            SpawnProjectile();
        }
            
    }

    void SpawnProjectile()
    {
        GameObject proj = Instantiate(projPrefab, transform.position, Quaternion.Euler(Vector3.zero));

        Projectile projComp = proj.GetComponent<Projectile>();
        projComp.direction = directionToTarget;

        projComp.damage = projDamage;
        projComp.speed = projSpeed;
    }
}
