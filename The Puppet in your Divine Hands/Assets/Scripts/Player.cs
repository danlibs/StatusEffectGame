using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public Transform FeetPosition;
    public float CheckRadius;
    public LayerMask IsGround;
    public float JumpTime;

    private Rigidbody2D rb;
    private Animator anim;
    private float movement;
    private bool isGrounded;
    private bool jump = false;
    private bool isJumping;
    private bool facingRight;
    private float jumpTimeCounter;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        anim.SetInteger("Speed", Mathf.RoundToInt(movement));

        isGrounded = Physics2D.OverlapCircle(FeetPosition.position, CheckRadius, IsGround);
        anim.SetBool("Jump", !isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }

        if (movement > 0 && facingRight)
        {
            Flip();
        }
        else if (movement < 0 && !facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * Speed * Time.fixedDeltaTime, rb.velocity.y);

        Vector2 jumpHeight = Vector2.up * (JumpForce + 5) * Time.fixedDeltaTime;
        if (jump)
        {
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
            isJumping = true;
            jumpTimeCounter = JumpTime;
            jump = false;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;
                rb.AddForce(jumpHeight + (jumpHeight * jumpTimeCounter), ForceMode2D.Impulse);
                
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        this.transform.Rotate(0, 180, 0);
    }
}
