using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody2D rb;
    public SpriteRenderer skin;
    public Animator anim;

    [Header("Configurações de Movimento")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Configurações de Chão")]
    private bool isGrounded;
    public float groundCheckRadius = 0.1f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Movimento
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (move > 0)
        {
            if (isGrounded)
                anim.Play("pRun", 0);
            
            skin.flipX = false;
        }
        else if (move < 0)
        {
            if (isGrounded)
                anim.Play("pRun", 0);

            skin.flipX = true;
        }
        else
        {
            if (isGrounded)
                anim.Play("pIdle", 0);
        }

        if (rb.velocity.y >= 0 && !isGrounded)
        {
            anim.Play("pJump", 0);
        }
        if (rb.velocity.y < 0 && !isGrounded)
        {
            anim.Play("pFall", 0);
        }

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}