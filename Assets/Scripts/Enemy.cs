using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator animator;
    public int maxHealth = 50;
    private int currentHealth;

    private Health health;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bomb;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage;

    [SerializeField] HPbar HP;

    private float cooldownTimer = Mathf.Infinity;

    public bool isAttacking = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        HP = GetComponentInChildren<HPbar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        // Attack only when the player is spotted
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("meleeAttack");
                isAttacking = true;
            }
        }
        if (!PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                isAttacking = false;
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.size.x * range, 
        boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        if (hit.collider != null)
        {
            health = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HP.UpdateHP(currentHealth, maxHealth);
        // Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy has died");

        // Die animation
        animator.SetBool("IsDead", true);


        // Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public void delete()
    {
        Destroy(gameObject);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            health.TakeDamage(damage);
        }
    }
}
