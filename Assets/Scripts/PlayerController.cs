using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsBase
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    public KeyManager km;
    private bool noMove = false;
    public GameObject respawnPoint;

    AudioManager audioManager;

    public void SetRespawnPoint(GameObject point)
    {
        respawnPoint = point;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (noMove)
        {
            desiredx = 0f;
            velocity = Vector2.zero;
            return;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        desiredx = 0;
        if (horizontalInput > 0) desiredx = 6f;
        if (horizontalInput < 0) desiredx = -6f;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = 15f;
            audioManager.PlaySFX(audioManager.jumpSFX);
        }

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
            audioManager.PlaySFX(audioManager.collectingSFX);
        }

        if (other.gameObject.CompareTag("EndPoint") && km.keyCount == 3)
        {
            if (noMove) return;
            noMove = true;
            audioManager.PlaySFX(audioManager.leavingSFX);
            animator.SetTrigger("isComplete");
        }
    }

    public void Leave()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public void Die()
    {
        if (noMove) return;
        noMove = true;
        audioManager.PlaySFX(audioManager.dyingSFX);
        animator.SetTrigger("isDead");
    }

    public void DeathAnimationDone()
    {
        transform.position = respawnPoint.transform.position;
        noMove = false;
        animator.SetTrigger("Revive");
    }
}
