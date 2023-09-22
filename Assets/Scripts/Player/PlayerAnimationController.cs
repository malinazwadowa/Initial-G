using UnityEngine;
//NOTE: TO BE UPDATED FOR ENEMY AS WELL NO NEED TO KEEP IT ONLY FOR PLAYER. UNLESS ENEMIES WILL BE ANIMIATED DIFFERENTLY. 
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovementController movementController;
    private PlayerData playerData;
    private SpriteRenderer[] spriteRenderers;

    public void Init(Animator animator, PlayerMovementController movementController, PlayerData playerData, SpriteRenderer[] spriteRenderers)
    {
        this.animator = animator;
        this.movementController = movementController;
        this.playerData = playerData;
        this.spriteRenderers = spriteRenderers;
    }

    public void SetAnimationVelocity(Vector2 input)
    {
        if(input.x == 0 && input.y == 0)
        {
            FlipSpriteRenderers(movementController.LastNonZeroVelocity);

            animator.SetBool("isIdling", true);
            animator.SetBool("isWalking", false);

            animator.SetFloat("HorizontalVelocity", movementController.LastNonZeroVelocity.x);
            animator.SetFloat("VerticalVelocity", movementController.LastNonZeroVelocity.y);
            return;
        }

        FlipSpriteRenderers(input);

        animator.SetBool("isWalking", true);
        animator.SetBool("isIdling", false);

        float velocityValue = (movementController.runRatio * movementController.speedModifiers) / (playerData.baseRunRatio * 2);
        float velocityX = Mathf.Lerp(0, 2, velocityValue * Mathf.Abs(input.x)) * Mathf.Sign(input.x);
        float velocityY = Mathf.Lerp(0, 2, velocityValue * Mathf.Abs(input.y)) * Mathf.Sign(input.y);
        
        animator.SetFloat("HorizontalVelocity", velocityX);
        animator.SetFloat("VerticalVelocity", velocityY);
    }

    public void FlipSpriteRenderers(Vector2 input)
    {
        if (input.x > 0)
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

    }
}
