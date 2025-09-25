using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    [Range(1, 20)]
    public float smoothFactor;
    public Vector3 minValue, maxValue;

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(desiredPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(desiredPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(desiredPosition.z, minValue.z, maxValue.z)
        );
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
