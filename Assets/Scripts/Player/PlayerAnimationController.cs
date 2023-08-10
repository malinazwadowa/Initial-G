using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovementController movementController;
    private PlayerData playerData;
    public void Init(Animator animator, PlayerMovementController movementController, PlayerData playerData)
    {
        this.animator = animator;
        this.movementController = movementController;
        this.playerData = playerData;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(movementController.lastDirection);
        SetMovementAnimation(movementController.GetCurrentMovementSpeed());
    }
    private void SetMovementAnimation(float currentSpeed)
    {
        animator.SetInteger(PlayerAnimatorParameters.DIRECTION_ID, movementController.lastDirection);
        if (currentSpeed == 0)
        {
            animator.SetBool(PlayerAnimatorParameters.IsIdling, true);
            animator.SetBool(PlayerAnimatorParameters.IsWalking, false);
            animator.SetBool(PlayerAnimatorParameters.IsRunning, false);
        }
        if (currentSpeed >= playerData.baseSpeed && movementController.GetCurrentMovementSpeed() > 0)
        {
            animator.SetBool(PlayerAnimatorParameters.IsIdling, false);
            animator.SetBool(PlayerAnimatorParameters.IsWalking, true);
            animator.SetBool(PlayerAnimatorParameters.IsRunning, false);
        }
        if (currentSpeed >= playerData.baseSpeed * playerData.runRatio * 0.8f)
        {
            animator.SetBool(PlayerAnimatorParameters.IsIdling, false);
            animator.SetBool(PlayerAnimatorParameters.IsWalking, false);
            animator.SetBool(PlayerAnimatorParameters.IsRunning, true);
        }
    }
}
