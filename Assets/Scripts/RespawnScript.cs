using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnPoint;

    private PlayerController pcontroller;
    // Start is called before the first frame update
    void Start()
    {
        pcontroller = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pcontroller.SetRespawnPoint(respawnPoint);
            pcontroller.Die();
        }
    }
}
