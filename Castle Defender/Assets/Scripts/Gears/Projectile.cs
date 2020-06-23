using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;
    public float rayDist;
    public LayerMask whatIsSolid;
    [HideInInspector] public float speed;
    [HideInInspector] public int damage;
    [HideInInspector] public float knockback;

    [HideInInspector] public Vector2 direction;

    public enum Origin
    {
        Player,
        Enemy
    }

    public Origin origin;

    void Start()
    {
        Invoke("DestroyProj", lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, rayDist, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (origin == Origin.Player)
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    hitInfo.collider.GetComponent<IDamageable>().GetDamage(damage);
                    hitInfo.collider.GetComponent<Rigidbody2D>().AddForce(direction * knockback, ForceMode2D.Impulse);
                }
            }
            else if (origin == Origin.Enemy)
                hitInfo.collider.GetComponent<IDamageable>().GetDamage(damage);

            DestroyProj();
        }

    }

    void DestroyProj()
    {
        Destroy(gameObject);
    }
}
