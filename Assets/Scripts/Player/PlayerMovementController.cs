using UnityEngine;

/// <summary>
/// 
/// lastDirection do wyjebania!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
///
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    public Vector2 CurrentVelocity { get; private set; }

    private PlayerInputActions inputActions;
    private PlayerData playerData;
    private Rigidbody2D rigidBody;
    private SpriteRenderer[] spriteRenderers;

    [HideInInspector] public int lastDirection = 3;

    private float currentSpeed;
    private float bonusSpeedRatio = 1;
    private float bonusSlowRatio = 1;
    private float runRatio = 1;
    
    public void Init(
        PlayerInputActions inputActions,
        PlayerData playerData,
        Rigidbody2D rigidBody,
        SpriteRenderer[] spriteRenderers)
    {
        this.inputActions = inputActions;
        this.playerData = playerData;
        this.rigidBody = rigidBody;
        this.spriteRenderers = spriteRenderers;
    }

    public float GetCurrentMovementSpeed()
    {
        return currentSpeed;
    }

    public void MovePlayer(Vector2 direction)
    {
        CurrentVelocity = direction * playerData.baseSpeed * runRatio * Time.deltaTime;
        rigidBody.velocity = CurrentVelocity;
    }

    public void SetRun(bool runAction)
    {
        if (runAction)
        {
            runRatio = playerData.runRatio;
        }
        else
        {
            runRatio = 1;
        }
    }


    //Moves player with desired speed according to provided input, updates direction player should be facing towards.
    /*
    public void MovePlayer(float speed)
    {
        Vector3 input = inputActions.Player.Movement.ReadValue<Vector2>();
        animator.SetInteger(PlayerAnimatorParameters.DIRECTION_ID, GetFacingDirection(input));
        if(input.x == 0) {input.x = 0.000001f;} //Workaround due to underlying unity issue and NavmeshPlus ~. 
        agent.Move(Time.deltaTime * speed * playerData.moveRatio * input);
    } */
    /*
     * Returns one of 4 values based on direction towards provided vector 
     * 1 - Up
     * 2 - Right
     * 3 - Down
     * 4 - Left
     * Depending on the angle, returns the corresponding direction code and flips the 'spriteRenderer' if needed.
     */
    public int GetFacingDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(!(direction.x == 0 && direction.y == 0))
        {
            switch (angle)
            {
                case 0:
                    lastDirection = 2;
                    break;
                case -45:
                    lastDirection = 2;
                    break;
                case -90:
                    lastDirection = 3;
                    break;
                case -135:
                    lastDirection = 4;
                    break;
                case 180:
                    lastDirection = 4;
                    break;
                case 135:
                    lastDirection = 4;
                    break;
                case 90:
                    lastDirection = 1;
                    break;
                case 45:
                    lastDirection = 2;
                    break;
                default:
                    break;
            }
        }

        if (lastDirection == 2)
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.flipX = false;
            }
        }
        
        return lastDirection;
    }
}
