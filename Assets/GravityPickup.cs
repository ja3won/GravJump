using UnityEngine;

// Minimal pickup: when the player enters, show the slider and bind it to the player's PhysicsBase
public class GravityPickup : MonoBehaviour
{
    public GameObject gravityPanel;
    public GravitySliderController sliderController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var physics = other.GetComponent<PhysicsBase>();
        if (physics == null) return;
        if (gravityPanel == null || sliderController == null) return;

        gravityPanel.SetActive(true);
        sliderController.target = physics;

        // Consume the pickup so it doesn't remain in the world
        Destroy(gameObject);
    }
}
