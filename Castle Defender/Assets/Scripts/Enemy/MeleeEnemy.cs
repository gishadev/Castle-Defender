using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Melee Variables")]
    public int damage;
    public LayerMask followLayer;
    public LayerMask attackLayers;
    [Space]
    public float viewRadius = 3f;
    public float attackRadius = 2f;

    void Update()
    {
        SearchTargetToFollow();

        if (CheckForAttack())
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
            targetToAttack = coll.transform;
            return true;
        }
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
            targetToAttack.GetComponent<IDamageable>().GetDamage(damage);
    }
}
