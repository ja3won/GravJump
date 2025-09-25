using UnityEngine;
using UnityEngine.UI;

public class GravitySliderController : MonoBehaviour
{
    public Slider gravitySlider;
    public PhysicsBase target;

    void Update()
    {
        gravitySlider.interactable = target.grounded;
        target.gravityFactor = gravitySlider.value;
    }
}