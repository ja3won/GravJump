using UnityEngine;

public class EnemyController : PhysicsBase
{
    [Header("Saw Settings")]
    public float distance = 3f;
    public float speed = 2f;  

    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
        desiredx = 0f;
        gravityFactor = 0f;
        velocity = Vector2.zero;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, distance * 2f) - distance;
        Vector3 pos = transform.position;
        pos.x = _startPos.x + offset;
        pos.y = _startPos.y;
        transform.position = pos;

        desiredx = 0f;
        velocity.x = 0f;
        velocity.y = 0f;
    }
}
