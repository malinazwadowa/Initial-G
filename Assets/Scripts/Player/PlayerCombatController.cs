using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// TODO
/// 
/// THIS CLASS SHOULD NOT BE A SINGLETON BY ANY MEANS
/// 
/// PLAYER COMBAT CONTROLLER SHOULD NOT BE RESPONSIBLE FOR MOVING THE BOMB, THE BOMB ITSELF SHOULD TAKE CARE OF ITS MOVEMENT
/// 
/// I DONT SEE A POINT OF HAVING ThrowType ENUM IF WE CAN THROW THE BOMB IN THE DIRECTION OF PLAYER LAST MOVEMENT DIRECTION
/// 
/// FUNCTIONS LIKE AttackAnimationEnd() SHOULD BE NAMED OnAttackAnimationEnd()
/// 
/// 
/// 
/// </summary>
public class PlayerCombatController : MonoBehaviour
{
    private enum ThrowType
    {
        Horizontal,
        Vertical
    }

    private Player player;

    [SerializeField] private float attackRange = 0.9f;
    [SerializeField] private float attackDamage = 30;
    //[SerializeField] private float attackDelay = 2;
    private Vector3 attackAreaOffset;
    private float timeSinceLastAttack;

    public LayerMask enemyLayers;
    public Transform AttackArea;
    public GameObject thrownBomb;
    public GameObject bomb;

    public static PlayerCombatController Instance { get; private set; }
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

        //player = GetComponent<Player>();
    }
  
    private void Update()
    {
        /*timeSinceLastAttack += Time.deltaTime;
        //Performs attack conditions are met.
        if (player.playerInputActions.Player.Attack.WasPerformedThisFrame() timeSinceLastAttack > attackDelay && player.playerData.attackingAllowed)
        {
            Attack();
            timeSinceLastAttack = 0;
        } */
    }

    //Performs attack. Deals damage to all enemies in area in front of player.
    public void Attack()
    {
        //player.animator.SetBool(PlayerAnimatorParameters.IsAttacking, true);

       
        int dupcia = 1;
        //Checks in which direction attack should be performed and offsets the area accordingly.
        switch (dupcia)
        {
            case 1:
                attackAreaOffset = new Vector3(0, 1.1f);
                break;
            case 2:
                attackAreaOffset = new Vector3(0.6f, 0.55f);
                break;
            case 3:
                attackAreaOffset = new Vector3(0f, -0.1f);
                break;
            case 4:
                attackAreaOffset = new Vector3(-0.6f, 0.55f);
                break;
        }
        //Detects enemies and deals damage. There seems to be a bug when multiple enemies are hit at once, it multiplies damage.
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(AttackArea.position + attackAreaOffset, attackRange, enemyLayers);
        //Deal damage to each enemy.
        foreach (Collider2D enemy in enemiesHit)
        {
            //enemy.GetComponent<EnemyCombatController>().GetDamaged(attackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackArea.position + attackAreaOffset, attackRange);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Picks up bomb if conditions are met.
        if (collision.CompareTag("BombGround")  /*player.inputActions.Player.PickUp.IsPressed()*/)
        {
            Destroy(collision.gameObject);
            //player.StateMachine.ChangeState(player.CarryingStance);
        }
    }
    //Drops bomb if player is carrying it.
    public void DropBomb()
    {
        float speed = 3;
        //Adjusted destination for the bomb.
        Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y - 0.42f, 0);
        GameObject newBomb = Instantiate(bomb, player.transform.position, thrownBomb.transform.rotation);
        StartCoroutine(DropBomb());

        //Bomb movement.
        IEnumerator DropBomb()
        {
            while (Vector2.Distance(newBomb.transform.position, target) > 0.01)
            {
                newBomb.transform.position = Vector3.MoveTowards(newBomb.transform.position, target, speed * Time.deltaTime);
            }
            yield return null;
        }
    }
    //Throws bomb in front of the player depending on the charge time, 4 possible directions.
    //Allowes to change direction while charging the throw.
    public void ThrowBomb(float chargeTime)
    {

        //AudioManager.Instance.PlaySound(AudioManagerClips.ThrowFyk, player.audioSource);
        float maxChargeTime = 3.5f;
        float minChargeTime = 0.5f;

        chargeTime = Mathf.Clamp(chargeTime, minChargeTime, maxChargeTime);

        float speed = 6;
        ThrowType throwType = 0;
        Vector3 throwOffset = Vector3.zero;
        Vector3 startPos = player.transform.position;
        float throwPower = chargeTime * 3;
        int dupa = 1;
        //Checks if the bomb will be thrown horizontally or vertically.
        switch (dupa)
        {
            case 1:
                throwOffset = new Vector3(0, throwPower);
                throwType = ThrowType.Vertical;
                break;
            case 2:
                throwOffset = new Vector3(throwPower, 0f);
                throwType = ThrowType.Horizontal;
                break;
            case 3:
                throwOffset = new Vector3(0f, -throwPower);
                throwType = ThrowType.Vertical;
                break;
            case 4:
                throwOffset = new Vector3(-throwPower, 0f);
                throwType = ThrowType.Horizontal;
                break;
        }

        Vector3 target = player.transform.position + throwOffset;
        GameObject newBomb = Instantiate(thrownBomb, player.transform.position, thrownBomb.transform.rotation);
        float entireDistance = Vector2.Distance(newBomb.transform.position, target);
        StartCoroutine(MoveBomb());

        //COROUTINES SHOULD NOT BE NESTED!!!!!!!!!!!!!!!!!
        IEnumerator MoveBomb()
        {
            while (Vector2.Distance(newBomb.transform.position, target) > 0.01f)
            {
                if (throwType == ThrowType.Vertical)
                {
                    //Moves the bomb.
                    newBomb.transform.position = Vector3.MoveTowards(newBomb.transform.position, target, speed * Time.deltaTime);

                    //Calculates arcHeight value based on the travelled distance, max value is reached around half of the entire distnace throw, zero on start and end.
                    float pathY = Mathf.MoveTowards(newBomb.transform.position.y, target.y, speed * Time.deltaTime);
                    float arcHeight =  entireDistance * (pathY - startPos.y) * (pathY - target.y) / (-1 * entireDistance * entireDistance);
                    
                    //Adjusts the scale of the bomb based on the calculated arcHeight in order to simulate the bomb travelling in arc.
                    newBomb.transform.localScale = new Vector3(1f + arcHeight/4, 1f + arcHeight/4, 1);
                }

                if (throwType == ThrowType.Horizontal)
                {
                    //Calculates arcHeight value based on the travelled distance, max value is reached around half of the entire distnace throw, zero on start and end.
                    float pathX = Mathf.MoveTowards(newBomb.transform.position.x, target.x, speed * Time.deltaTime);
                    float arcHeight = entireDistance * (pathX - startPos.x) * (pathX - target.x) / (-1 * entireDistance * entireDistance);
                    
                    //Adjusts .Y position of the bomb to make it travel in an arc.
                    Vector3 movePosition = new Vector3(pathX, startPos.y + arcHeight, newBomb.transform.position.z);

                    //Moves the bomb.
                    newBomb.transform.position = movePosition;
                }
                yield return null;
            }
            //Assures that targer destination is reached in the end.
            newBomb.transform.position = target;

            //Calls for BombIsGrounded() to start explosion.
            BombController bombController = newBomb.GetComponent<BombController>();
            bombController.BombIsGrounded();
        }
    }
    public void GetDamaged(float dmgAmount)
    {
        
        //PlayerHealthController.Instance.SubstractHealth(dmgAmount);
        //player.animator.StopPlayback();
    }
    //Methods with *Animation* in name are used by animation events.
   /* public void AttackAnimationEnd()
    {
        player.animator.SetBool(PlayerAnimatorParameters.IsAttacking, false);
        player.StateMachine.ChangeState(player.IdleState);
    }
    public void ThrowAnimationEnd()
    {
        player.animator.SetBool(PlayerAnimatorParameters.IsThrowing, false);
        player.StateMachine.ChangeState(player.IdleState); 
    }
    //Checks players health after being hit, kills player if health reaches 0.
    public void HitAnimationEnd()
    { /*
        player.animator.SetBool(PlayerAnimatorParameters.IsHit, false);
        if (PlayerHealthController.Instance.CurrentHealth()<= 0)
        {
            //Player would go to Dead state instead but its not implemented yet.
            player.StateMachine.ChangeState(player.IdleState);
        }
        else
        {
            player.StateMachine.ChangeState(player.IdleState); 
        }
    } */
}