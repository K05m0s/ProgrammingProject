using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Animator animator;
    Controls control;

    protected Projectile projectileScript;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    private float nextAttackTime = 0f;
    public bool canMove_gun;
    public float attackRate = 1f;

    private void Start()
    {
        canMove_gun = true;
        control = gameObject.GetComponent<Controls>();
    }
    private void Update()

    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && control.isRolling == false && control.canShoot == true)
            {
                Shoot();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else
            {
                canMove_gun = true;
            }


        }
        void Shoot()
        {
            canMove_gun = false;
            animator.SetTrigger("IsShooting");
            animator.SetBool("IsRunning", false);
            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }

    }     
}
