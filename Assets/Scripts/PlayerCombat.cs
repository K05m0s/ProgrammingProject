using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    Health HP;

    Controls control;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float stunDamageAmount = 1f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 25;
    public bool canMove;
    public bool canRoll_CB;
    public float attackRate = 1f;
    private float nextAttackTime = 0f;
    public int attackCount =0;

    private attackDetails attackDetails;

    // Update is called once per frame

    private void Start()
    {
        canMove = true;
        canRoll_CB = true;
        control = gameObject.GetComponent<Controls>();
        rb = GetComponent<Rigidbody2D>();
        HP = GetComponent<Health>();
    }
    void Update()
    {
        if (attackCount >= 2)
        {
            attackCount = 0;
        }
        
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && control.isRolling == false)
            {
                Attack();
                attackCount = attackCount + 1;
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else
            {
                canMove = true;
                canRoll_CB = true;
            }
        }  
    }

    void Attack()
    {
        // Play an attack animation
        canMove = false;
        canRoll_CB = false;
        animator.SetFloat("yVelocity", 0);
        animator.SetTrigger("Attack");
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        attackDetails.damageAmount = attackDamage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = stunDamageAmount;

        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.transform.parent.SendMessage("Attack", attackDetails);
        }
    }

    private void Damage(attackDetails attackDetails)
    {
        HP.TakeDamage(attackDetails.damageAmount);
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
                return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
