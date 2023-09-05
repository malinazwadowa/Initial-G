using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovementController movementController;
    private PlayerData playerData;
    private SpriteRenderer[] spriteRenderers;

    public Vector2 lastVelocity = new Vector2(0, -1);
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
            FlipSpriteRenderers(lastVelocity);

            animator.SetBool("isIdling", true);
            animator.SetBool("isWalking", false);

            animator.SetFloat("HorizontalVelocity", lastVelocity.x);
            animator.SetFloat("VerticalVelocity", lastVelocity.y);
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

        lastVelocity = new Vector2(velocityX, velocityY);
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

    public Quaternion GetRotationFromVelocity()
    {
        float angle = Mathf.Atan2(lastVelocity.y, lastVelocity.x);
        //Debug.Log($"Angle to: {angle}");
        return Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
    }
}
