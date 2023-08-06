/// <summary>
/// TODO
/// 
/// THERE SHOULDNT BE A PLAYER STATE MACHINE NOR ENEMY STATE MACHINE, THERE SHOULD BE ONLY STATE MACHINE THAT PLAYER AND ENEMY WOULD USE
/// 
/// RIGHT NOW ChangeState(PlayerState state) FUNCTION IS CALLED FROM OUTSIDE OF THE STATE MACHINE - THE STATE MACHINE SHOULD DETERMINE IN WHICH STATE IT SHOULD BE BY ITSELF AND NO OTHER SCRIPT.
/// 
/// </summary>
public class PlayerStateMachine 
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
