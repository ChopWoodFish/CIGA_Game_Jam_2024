using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private bool _jumping;
    private Animator TheAnimator => GetComponent<Animator>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.Inst.gameStage != 2 || )
        {
            return;
        }
        
        Move();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (rb.velocity.y < 0) IsFell();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);



        if (moveInput != 0)
        {
            TheAnimator.SetBool("Walking", true);
            //int driection = moveInput > 0 ? 1 : -1;
            //if (transform.localScale.x * driection < 0)
            //{
            //    transform.localScale = new Vector3(transform.localScale.x * driection, transform.localScale.y, 1);
            //}

            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Sign(-moveInput) * Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
        else
        {
            TheAnimator.SetBool("Walking", false);
        }


    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
        TheAnimator.SetTrigger("Jumping");
        _jumping = true;
        SoundManager.instance.PlayStepSound();
    }

    private void IsFell()
    {
        if (rb.velocity.y < 0 && _jumping)
            TheAnimator.SetTrigger("IsFell");
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            _jumping = false;
            TheAnimator.SetTrigger("IsGround");
        }
    }
}
