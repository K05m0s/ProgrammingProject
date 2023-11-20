using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using UnityEngine.TextCore.Text;
using UnityEditor;
using System.Runtime.CompilerServices;

public class Controls : MonoBehaviour
{
    //Declaring variables
    PlayerCombat playerCombat;
    Gun gun;
    
    // Animation
    private Animator anim; 
    // Horizontal movement
    private float horizontal;
    private float speed = 15f;
    private bool isFacingRight = true;
    // Jumping
    private float jumpingPower = 16f;
    private bool doubleJump;
    // Roll mechanic
    private bool canRoll = true;
    public bool isRolling;
    private float rollPower = 4f;
    private float rollTime = 0.25f;
    private float rollRate = 1.5f;
    private float nextRolltime = 0f;
    // Coyote Time
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    // Jump Buffer
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    // Shooting mechanic
    public bool canShoot = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    // Fetches the animator, so it could be used in this script
    private void Start()
    {
        anim = GetComponent<Animator>();
        gun = gameObject.GetComponent<Gun>();
        playerCombat = gameObject.GetComponent<PlayerCombat>();
        int yourPlayerLayer = LayerMask.NameToLayer("Actor");
        int yourEnemyLayer = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGrounded()) // Checks if the player is colliding with the ground layer
        {
            coyoteTimeCounter = coyoteTime; // Sets the coyoteTimeCounter to the value of the pre-set coyoteTime
            canShoot = true;
        }
        else // If the player is not touching the ground, decrease the coyoteTimeCounter by current time since last frame
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump")) // Checks if the user has pressed the jump button
        {
            jumpBufferCounter = jumpBufferTime; // Sets the jumpBufferCounter to the value of the pre-set jumpBufferTime
        }
        else // If the player is not hitting the jump button, decrease the jumpBufferCounter by current time since last frame
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        
        if (Time.time >= nextRolltime) // Only allows roll script if the roll is within cooldown and if the player is not attacking 
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll == true && playerCombat.canRoll_CB == true) // Checks if the user presses the shift key
            {
                StartCoroutine(Roll());
                nextRolltime = Time.time + 1f / rollRate; // Resets the roll cooldown
            }
        }
        if(!IsGrounded())
        {
            canShoot = false;
        }

        // Prevents the player from moving and jumping while rolling
        if (isRolling)
        {
            return;
        }

        // Prevents player from moving
        if (gun.canMove_gun == false || playerCombat.canMove == false)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
            return;
        }
       else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
     
        // Set doublejump to false as soon as we are gounded and the jump button is not pressed
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
            anim.SetFloat("yVelocity", 0);
        }

        //checks if jumpBufferCounter is greater than 0
        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f || doubleJump) // Checks whether the user is within the jumpBufferCounter or can double jump
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // Accelerates the player upwards by the value of jumpingPower, and keeps the player in the same horizontal axis 
                doubleJump = !doubleJump;
                // Resets both counters
                coyoteTimeCounter = 0f;
                jumpBufferCounter = 0f;
            }
        }
       
        //Calls the flip function
        if(horizontal > 0 && !isFacingRight) // Flip the sprite if the player is moving and is not facing right
        {
            Flip();
        }
        else if (horizontal < 0 && isFacingRight) // Flip the sprite if the player is moving and is facing right
        {
            Flip();
        }

        //Checks if the character is running, then sets the animation condition accordingly
        if (horizontal == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }

        if(!IsGrounded())
        {
            anim.SetFloat("yVelocity", rb.velocity.y);
            anim.SetBool("Grounded", false);
            canRoll = false;
        }
        else
        {
            canRoll = true;
        }
    }

    // Declares the final movement speed of the player
    private void FixedUpdate()
    {
        if (gun.canMove_gun == false || playerCombat.canMove == false)
        {
        return;
        }
        //Same function as before
        if (isRolling)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // Sets the horizontal player velocity to the speed desired, and keeps the vertical movement as it is
    }
    
    // Checks if the player is touching the ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Flips the character sprite in the direction of movement
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) 
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(Vector2.up * 180); // Flips the characters sprite in the other direction
        }
    }

    private IEnumerator Roll()
    {
        Physics2D.IgnoreLayerCollision(8, 7, true); // Makes sure the character ignores the enemy collider during its roll, so it can pass through them
        anim.SetTrigger("IsRolling");// Triggers the roll animation
        isRolling = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        // Checks the direction the user is facing to determine the direction of the roll
        if(isFacingRight)
        {
            rb.velocity = new Vector2(transform.localScale.x * rollPower, 0f);
        }
        if (!isFacingRight)
        {
            rb.velocity = new Vector2(-transform.localScale.x * rollPower, 0f);
        }
        yield return new WaitForSeconds(rollTime);
        rb.gravityScale = originalGravity;
        isRolling = false;
        anim.SetFloat("yVelocity", 0);
        Physics2D.IgnoreLayerCollision(8, 7, false); // Makes sure the enemy collider is turned back on after the roll has finished
    }
}
