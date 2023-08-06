using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    [Header("Settings")]
    [SerializeField] private Transform playerSpawnPosition;

    [Header("Dependencies")]
    [SerializeField] private Player playerPrefab;

    private Player player;

    protected override void Awake()
    {
        base.Awake();
        //DO SOMETHING
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, playerSpawnPosition);
    }

    public Player GetPlayer()
    {
        return player;
    }
}
