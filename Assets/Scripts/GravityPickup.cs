using UnityEngine;

public class GravityPickup : MonoBehaviour
{
    public GameObject gravityPanel;
    public AudioManager audioManager;
    public GravitySliderController sliderController;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var physicsBase = other.GetComponent<PhysicsBase>();
        sliderController.target = physicsBase;
        audioManager.PlaySFX(audioManager.collectingSFX);
        gravityPanel.SetActive(true);
        Destroy(gameObject);
    }
}
