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

    void Movement(Vector2 move, bool horizontal)
    {
        if (move.magnitude < 0.000001f) return;
        RaycastHit2D[] hits = new RaycastHit2D[16];
        int cnt = GetComponent<Rigidbody2D>().Cast(move, hits, move.magnitude + 0.01f);
        for (int i = 0; i < cnt; i++)
        {
            if (Mathf.Abs(hits[i].normal.x) > 0.3f && horizontal)
            {
                return;
            }

            if (Mathf.Abs(hits[i].normal.y) > 0.3f && !horizontal)
            {
                velocity.y = 0f;
                if (hits[i].normal.y > 0.3f) grounded = true;
                return;
            }
        }
        transform.position += (Vector3)move;
    }

    void FixedUpdate()
    {
        grounded = false;
        Vector2 acceleration = 9.81f * Vector2.down * gravityFactor;
        velocity += acceleration * Time.fixedDeltaTime;
        Vector2 move = velocity * Time.fixedDeltaTime;
        Movement(new Vector2(move.x, 0), true);
        Movement(new Vector2(0, move.y), false);

        if (!grounded)
        {
            RaycastHit2D[] hits = new RaycastHit2D[16];
            int cnt = GetComponent<Rigidbody2D>().Cast(Vector2.down, hits, Mathf.Max(0.1f, move.magnitude + 0.01f));
            for (int i = 0; i < cnt; i++)
            {
                if (hits[i].normal.y > 0.3f) grounded = true;
            }
        }

        if (grounded)
        {
            velocity.x = desiredx;
        }
    }
}
