using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    [Tooltip("Optional: assign a visual to enable when activated")] public GameObject activatedVisual;
    private bool _activated;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponentInParent<PhysicsBase>();
        if (player == null) return;
        if (_activated) return;

        CheckpointManager.Instance.SetCheckpoint(transform.position);
        _activated = true;
        if (activatedVisual != null) activatedVisual.SetActive(true);
    }
}
