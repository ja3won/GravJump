using UnityEngine;

public class RespawnScript : MonoBehaviour
{

    public GameObject player;
    public GameObject respawnPoint;

    void Start()
    {
        
    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
        }
    }
}
