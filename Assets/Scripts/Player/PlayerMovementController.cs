using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 LastNonZeroVelocity { get; private set; } = Vector2.up;
    
    private SO_PlayerParameters playerData;
    private Rigidbody2D rigidBody;
    
    public float runRatio = 1;
    public float speedModifiers = 1;
    
    public void Initialize(SO_PlayerParameters playerData, Rigidbody2D rigidBody)
    {
        this.playerData = playerData;
        this.rigidBody = rigidBody;
    }
    public void MovePlayer(Vector2 direction)
    {
        CurrentVelocity = playerData.baseSpeed * runRatio * speedModifiers * Time.deltaTime * direction;

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
