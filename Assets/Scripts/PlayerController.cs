using UnityEngine;

public class PlayerController : PhysicsBase
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    public KeyManager km;
    private bool isDying = false; // lock controls while death anim plays
    public GameObject respawnPoint;

    public void SetRespawnPoint(GameObject point)
    {
        respawnPoint = point;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDying)
        {
            desiredx = 0f;
            velocity = Vector2.zero;
            return;
        }

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            km.keyCount++;
        }
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;
        animator.SetTrigger("isDead");
    }

    public void DeathAnimationDone()
    {
        transform.position = respawnPoint.transform.position;
        isDying = false;
        animator.SetTrigger("Revive");
    }
}
