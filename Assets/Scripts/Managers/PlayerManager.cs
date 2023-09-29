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
        
        SpawnPlayer();
    }

    private void Start()
    {
        
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, playerSpawnPosition);
    }

    public Player GetPlayer()
    {
        return player;
    }
    public Transform GetPlayersTransform()
    {
        return player.transform;
    }
    public Vector3 GetCurrentPlayersPosition()
    {
        return player.transform.position;
    }
    public Vector3 GetCurrentPlayerWeaponsPosition()
    {
        Transform weaponTransform = player.GetComponentInChildren<WeaponController>().transform;
        return weaponTransform.position;
    }
}
