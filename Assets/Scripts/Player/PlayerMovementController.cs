using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 LastNonZeroVelocity { get; private set; } = Vector2.up;
    
    private Rigidbody2D rigidBody;
    public SO_PlayerParameters playerData { get; private set; }
    public CharacterStats characterStats { get; private set; }

    public float runRatio = 1;
    
    public void Initialize(SO_PlayerParameters playerData, Rigidbody2D rigidBody, CharacterStats characterStats)
    {
        this.playerData = playerData;
        this.rigidBody = rigidBody;
        this.characterStats = characterStats;
    }
    public void MovePlayer(Vector2 direction)
    {
        CurrentVelocity = playerData.baseSpeed * runRatio * characterStats.moveSpeedModifier * Time.deltaTime * direction;

        if (CurrentVelocity.magnitude > 0f)
            LastNonZeroVelocity = CurrentVelocity;

        rigidBody.velocity = CurrentVelocity;
    }

    public void SetRun(bool runAction)
    {
        if (runAction)
        {
            runRatio = playerData.baseRunRatio;
        }
        else
        {
            runRatio = 1;
        }
    }
}
