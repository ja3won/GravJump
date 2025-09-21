using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsBase
{
    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        desiredx = 0;
        if (horizontalInput > 0) desiredx = 6f;
        if (horizontalInput < 0) desiredx = -6f;

        if (Input.GetButton("Jump") && grounded) velocity.y = 15f;

        if (animator != null)
        {
            animator.SetFloat("xVelocity", Mathf.Abs(velocity.x));
            animator.SetFloat("yVelocity", velocity.y);
            animator.SetBool("isJumping", !grounded);
        }

        if (grounded)
        {
            FlipSprite(horizontalInput);
        }
    }

    private void FlipSprite(float horizontalInput)
    {
        if (spriteRenderer == null) return;
        if ((isFacingRight && horizontalInput > 0f) || (!isFacingRight && horizontalInput < 0f))
        {
            isFacingRight = !isFacingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public void Collide(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lethal"))
        {
            Debug.Log("DED");
        }
    }

    public override void CollideHorizontal(Collider2D other)
    {
        Collide(other);
    }

    public override void CollideVertical(Collider2D other)
    {
        Collide(other);
    }
}
