using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private RespawnScript respawn;

    void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
