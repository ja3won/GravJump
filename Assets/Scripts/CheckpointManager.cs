using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager _instance;
    public static CheckpointManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("CheckpointManager");
                _instance = go.AddComponent<CheckpointManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private Vector3 _lastCheckpoint;
    private bool _hasCheckpoint;
    private PhysicsBase _player;
    private Vector3 _initialPlayerPos;

    public void RegisterPlayer(PhysicsBase player)
    {
        _player = player;
        _initialPlayerPos = player.transform.position;
        if (!_hasCheckpoint)
        {
            _lastCheckpoint = _initialPlayerPos;
            _hasCheckpoint = true;
        }
    }

    public void SetCheckpoint(Vector3 position)
    {
        _lastCheckpoint = position;
        _hasCheckpoint = true;
    }

    public void Respawn()
    {
        if (_player == null)
        {
            return;
        }
        if (!_hasCheckpoint)
        {
            _lastCheckpoint = _initialPlayerPos;
            _hasCheckpoint = true;
        }
        _player.transform.position = _lastCheckpoint;
        _player.velocity = Vector2.zero;
        _player.desiredx = 0f;
    }
}
