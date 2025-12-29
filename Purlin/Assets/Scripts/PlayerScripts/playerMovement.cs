using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class playerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    
    public InputAction playerMovementX;
    public InputAction playerJump;
    public InputAction playerDash;

    private float moveSpeed = 5f;
    public bool canMove = true;
    
    //jump related stuff
    private float jumpSpeed = 20f;
    public Transform groundCheck;
    public float groundCheckRadius = .01f;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    public bool doubleJumpUnlocked = false;
    private bool canDJump = true;

    //dash related stuff
    private float dashSpeed = 10f;
    private float dashDuration = .2f;
    public bool dashUnlocked = true;
    public bool canDash = true;

    Vector2 moveDirection = Vector2.zero;
    public int facingDirection = 1;

    public Spell queuedSpell = null;//when inputs are put in during an animation, cast this next, helps flow of gameplay

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        playerMovementX.Enable();
        playerJump.Enable();
        playerDash.Enable();
    }

    private void OnDisable()
    {
        playerMovementX.Disable();
        playerJump.Disable();
        playerDash.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if(!canDJump && isTouchingGround)
        {
            canDJump = true;
        }
        
        //if jump input
        if (playerJump.WasPerformedThisFrame() && canMove)
        {
            if (isTouchingGround)
            {
                rb.linearVelocityY = jumpSpeed;
            }
            else if (doubleJumpUnlocked && canDJump)
            {
                Debug.Log("djump");
                //if in the air and double jump is availible
                canDJump = false;
                rb.linearVelocityY = jumpSpeed;
            }
        }
        moveDirection = playerMovementX.ReadValue<Vector2>();
        if(moveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
        } else if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
        }

        if (playerDash.WasPerformedThisFrame() && dashUnlocked && canDash && canMove)
        {
            StartCoroutine(Dash());
        }

        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocityY);
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        canMove = false;
        float startTime = Time.time;
        rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        while (Time.time < startTime + dashDuration){
            rb.linearVelocity = new Vector2(facingDirection > 0 ? dashSpeed : -dashSpeed, 0);
            yield return null;
        }

        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        canMove = true;
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, 0);
        bool touchedGround = isTouchingGround; //if on ground before cd is up, still reset dash
        yield return new WaitForSeconds(.2f);//abillity cd
        yield return new WaitUntil(()  => isTouchingGround || touchedGround);
        canDash = true;

    }
}
