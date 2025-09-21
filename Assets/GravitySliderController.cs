using UnityEngine;
using UnityEngine.UI;

// Minimal controller: slider controls target.gravityFactor
public class GravitySliderController : MonoBehaviour
{
    public Slider gravitySlider;
    public PhysicsBase target;

    void Update()
    {
        if (target == null || gravitySlider == null) return;
        // Disable interaction while airborne (not grounded)
        gravitySlider.interactable = target.grounded;
        target.gravityFactor = gravitySlider.value;
    }
}