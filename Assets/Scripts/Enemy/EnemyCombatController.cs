using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    private Enemy enemy;
    public LayerMask playerLayer;
    //public static EnemyCombatController Instance { get; private set; }
    private void Awake()
    {
        /*if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        } */

        enemy = GetComponent<Enemy>();
    }

    public void GetDamaged(float dmgAmount)
    {
        enemy.SubstractHealth(dmgAmount);
        enemy.animator.StopPlayback();
        enemy.animator.SetBool(EnemyAnimatorParameters.IsHit, true);
        enemy.StateMachine.ChangeState(enemy.HitState);
    }
    //Performs AOE attack, called by attack animation.
    public void AttackAOE()
    {
        float attackRange = 1;
        float attackDamage = 20;
        Collider2D[] aoe = Physics2D.OverlapCircleAll(enemy.transform.position, attackRange, playerLayer);
        
        foreach (Collider2D player in aoe)
        {
            player.GetComponent<PlayerCombatController>().GetDamaged(attackDamage);
        }
    }
    //Used for the animator logic.
    public void AttackAnimationEnd()
    {
        Debug.Log("dupa");
        enemy.animator.SetBool(EnemyAnimatorParameters.IsCombating, true);
        enemy.animator.SetBool(EnemyAnimatorParameters.IsAttacking, false);
    }

    //Called at the end of getting hit animation, checks if the enemy should die.
    public void HitAnimationEnd()
    {
        enemy.animator.SetBool(EnemyAnimatorParameters.IsHit, false);
        if (enemy.CurrentHealth() <= 0)
        {
            enemy.StateMachine.ChangeState(enemy.DeadState);
        }
        else
        {
            enemy.StateMachine.ChangeState(enemy.WanderState);
        }
    }
}
