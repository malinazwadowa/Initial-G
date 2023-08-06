using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO
/// 
/// THIS CLASS ALSO SHOULD NOT BE A SINGLETON BY ANY MEANS.
/// 
/// THIS CLASS SHOULD NOT BE NAMED PlayerDirectionController BUT RATHER A PlayerMovementController
/// 
/// PLAYER SHOULD NOT BE MOVED VIA NAV MESH AGENT BUT RATHER A SIMPLE TRANSFORMATION FUNCTION
/// </summary>
public class PlayerDirectionController : MonoBehaviour
{
    public int lastDirection = 3; //For attack animation
    private Player player;
    private SpriteRenderer[] spriteRenderers;

    public static PlayerDirectionController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        player = GetComponent<Player>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    /* Used before NavMesh pathafinding
     * public void MoveGamer(float speed)
    {
        Vector2 input = player.playerInputActions.Player.Movement.ReadValue<Vector2>();
        player.animator.SetInteger(PlayerAnimatorParameters.DirectionID, FacingDirection(input));
        playerRb.MovePosition(speed * player.moveRatio * Time.deltaTime * input + playerRb.position);
    } */
    //Moves player with desired speed according to provided input, updates direction player should be facing towards.
    public void MovePlayer(float speed)
    {
        Vector3 input = player.playerInputActions.Player.Movement.ReadValue<Vector2>();
        player.animator.SetInteger(PlayerAnimatorParameters.DIRECTION_ID, FacingDirection(input));
        if(input.x == 0) {input.x = 0.01f;} //Workaround due to underlying unity issue and NavmeshPlus ~. 
        player.agent.Move(Time.deltaTime * speed * player.playerData.moveRatio * input);
    }
    /*
     * Returns one of 4 values based on direction towards provided vector 
     * 1 - Up
     * 2 - Right
     * 3 - Down
     * 4 - Left
     * Depending on the angle, returns the corresponding direction code and flips the 'spriteRenderer' if needed.
     */
    public int FacingDirection(Vector2 direction)
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