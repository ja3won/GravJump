using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBase : MonoBehaviour
{
    public Vector2 velocity;
    public float gravityFactor;
    public float desiredx;
    public bool grounded;
    private bool wasGrounded; // track previous tick grounded state for air control lock

    // Start is called before the first frame update
    void Start()
    {

    }

    public virtual void CollideHorizontal(Collider2D other)
    {

    }

    public virtual void CollideVertical(Collider2D other)
    {

    }

    void Movement(Vector2 move, bool horizontal)
    {
        if (move.magnitude < 0.000001f) return;
        RaycastHit2D[] hits = new RaycastHit2D[16];
        int cnt = GetComponent<Rigidbody2D>().Cast(move, hits, move.magnitude + 0.01f);
        bool collision = false;
        for (int i = 0; i < cnt; i++)
        {
            if (Mathf.Abs(hits[i].normal.x) > 0.3f && horizontal)
            {
                collision = true;
                CollideHorizontal(hits[i].collider);
            }

            if (Mathf.Abs(hits[i].normal.y) > 0.3f && !horizontal)
            {
                velocity.y = 0f;
                if (hits[i].normal.y > 0.3f) grounded = true;
                collision = true;
                CollideVertical(hits[i].collider);
            }
        }
        if (collision) return;
        transform.position += (Vector3)move;
    }

    void FixedUpdate()
    {
        grounded = false;
        Vector2 acceleration = 9.81f * Vector2.down * gravityFactor;
        velocity += acceleration * Time.fixedDeltaTime;

        if (wasGrounded)
        {
            velocity.x = desiredx;
        }
        Vector2 move = velocity * Time.fixedDeltaTime;
        Movement(new Vector2(move.x, 0), true);
        Movement(new Vector2(0, move.y), false);

        if (!grounded)
        {
            RaycastHit2D[] hits = new RaycastHit2D[4];
            int cnt = GetComponent<Rigidbody2D>().Cast(Vector2.down, hits, 0.05f);
            for (int i = 0; i < cnt; i++)
            {
                if (hits[i].collider != null && hits[i].normal.y > 0.3f)
                {
                    grounded = true;
                    break;
                }
            }
        }

        if (grounded)
        {
            velocity.x = desiredx;
        }

        wasGrounded = grounded;
    }
}
