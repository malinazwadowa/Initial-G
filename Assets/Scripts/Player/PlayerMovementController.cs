using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Vector2 CurrentVelocity { get; private set; }
    
    private PlayerData playerData;
    private Rigidbody2D rigidBody;
    
    public float runRatio = 1;
    public float speedModifiers = 1;
    
    public void Init(PlayerData playerData, Rigidbody2D rigidBody)
    {
        this.playerData = playerData;
        this.rigidBody = rigidBody;
    }
    public void MovePlayer(Vector2 direction)
    {
        CurrentVelocity = playerData.baseSpeed * runRatio * speedModifiers * Time.deltaTime * direction;
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
