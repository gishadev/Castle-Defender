﻿using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;
    public float rayDist;
    public LayerMask whatIsSolid;
    [HideInInspector] public float speed;
    [HideInInspector] public int damage;

    float direction;

    void Start()
    {
        direction = PlayerController.Instance.transform.localScale.x;
        Invoke("DestroyProj", lifeTime);
    }

    void Update()
    {
        transform.Translate(transform.right * speed * direction * Time.deltaTime, Space.World);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, rayDist, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
                hitInfo.collider.GetComponent<IDamageable>().GetDamage(damage);

            DestroyProj();
        }

    }

    void DestroyProj()
    {
        Destroy(gameObject);
    }
}
