using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChaseAi : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    public Animator anim;
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.isAttacking = false;
    }

    private void Update()
    {
        if (enemy.isAttacking == true)
        {
            return;
        }

        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-6, 6, 6);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                anim.SetBool("running", true);
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(6, 6, 6);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                anim.SetBool("running", true);
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    transform.localScale = new Vector3(-6, 6, 6);
                    patrolDestination = 1;
                    anim.SetBool("running", true);
                }
            }
            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    transform.localScale = new Vector3(6, 6, 6);
                    patrolDestination = 0;
                    anim.SetBool("running", true);
                }
            }
        }
    }
}
