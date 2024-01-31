using UnityEngine;
using UnityEngine.Events;

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
        player = Instantiate(playerPrefab, playerSpawnPosition.position, default);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public PlayerInputActions GetPlayerInputActions()
    {
        return player.InputActions;
    }

    public PlayerInputController GetPlayerInputController()
    {
        return player.InputController;
    }

    public Transform GetPlayersCenterTransform()
    {
        Transform centerTransform = player.transform.Find("Center");

        if (centerTransform != null)
        {
            return centerTransform;
        }
        else
        {
            Debug.LogError("Player has no child object called Center");
            return null;
        }
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
