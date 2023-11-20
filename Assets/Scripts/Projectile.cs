using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private attackDetails attackDetails;

    private float speed = 40;

    private Rigidbody2D rb;

    private bool hasHitGround;

    private float attackDamage = 10;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform damagePosition;
    [SerializeField] private float damageRadius;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;
    }

    private void FixedUpdate()
    {
        if (!hasHitGround) 
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, playerLayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, groundLayer);

            attackDetails.damageAmount = attackDamage;

            if (damageHit)
            {
                damageHit.transform.parent.SendMessage("Attack", attackDetails);
                Destroy(gameObject);
            }
            
            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
