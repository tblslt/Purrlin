using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DashAndJump : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    Vector2 moveDirection = Vector2.zero;
    public int facingDirection = 1;

    ////move
    //private float moveSpeed = 5f;
    //public bool canMove = true;

    //dash
    private float dashSpeed = 10f;
    private float dashDuration = .2f;
    public bool dashUnlocked = true;
    public bool canDash = true;

    //jump
    private float jumpSpeed = 20f;
    public float groundCheckRadius = .5f;
    private bool isTouchingGround;
    public bool doubleJumpUnlocked = true;
    private bool canDJump = true;

    void TouchedGround()
    {
        canDash = true;
        canDJump = true;
    }
    
    void CastDash()
    {
        StartCoroutine(Dash());
    }

    void CancelDash()
    {
        StopCoroutine(Dash());
    }

    void CastJump()
    {
        if (isTouchingGround)
        {
            rb.linearVelocityY = jumpSpeed * Time.deltaTime;
        }
        else if (doubleJumpUnlocked && canDJump)
        {
            //if in the air and double jump is availible
            canDJump = false;
            rb.linearVelocityY = jumpSpeed * Time.deltaTime;
        }
    }

    IEnumerator Dash()
    {

        canDash = false;
        float startTime = Time.time;

        rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        while (Time.time < startTime + dashDuration)
        {
            rb.linearVelocity = new Vector2(facingDirection > 0 ? dashSpeed * Time.deltaTime : -dashSpeed * Time.deltaTime, 0);
            yield return null;
        }

        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        rb.linearVelocity = new Vector2(moveDirection.x, 0);
        bool touchedGround = isTouchingGround; //if on ground before cd is up, still reset dash
        yield return new WaitForSeconds(.2f);//abillity cd
        yield return new WaitUntil(() => isTouchingGround || touchedGround);
        canDash = true;

    }

}
