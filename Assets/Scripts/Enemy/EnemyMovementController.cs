using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public float detectionRadius;
    public float offsetDistanceFromPlayersPosition;
    
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    
    private LayerMask combinedMask;
    
    private float speed;
    private Transform playerTransform;
    private OccupiedSide occupiedSide;

    private Vector3 enemyAvoidanceOffset;
    private Vector3 directionTowardsPlayer;
    private Vector3 movementDirection;
    private Vector3 orderedDirection;
    private Vector3 knockbackVector;
    private List<Vector3> directionsAwayFromOtherEnemies = new List<Vector3>();

    private bool isGuided = false;
    private bool isKnockedback = false;
    private bool canWalkTowardsPlayer = false;


    private void OnEnable()
    {
        isGuided = false;
        canWalkTowardsPlayer = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        knockbackVector = Vector3.zero;
        isKnockedback = false;
    }

    public void Init(float speed)
    {
        this.speed = speed;
        playerTransform = PlayerManager.Instance.GetPlayersCenterTransform();
        combinedMask = CombineLayers(enemyLayer, obstacleLayer);
    }

    private void Update()
    {
        directionTowardsPlayer = (playerTransform.position - transform.position).normalized;
        
        ApplyMovement();
        HandleAvoidance(combinedMask);
    }

    public IEnumerator ApplyKnockback(float knockbackPower, Vector3 knockbackDirection)
    {
        if (isKnockedback) { yield break; }
       
        isKnockedback = true;
        float timer = 0;
        float knockbackDuration = knockbackPower / 16.7f;

        while (timer < knockbackDuration)
        {
            float knockbackToApply = knockbackPower * Time.deltaTime;

            knockbackVector = knockbackDirection * knockbackToApply;
            timer += Time.deltaTime;
            yield return null;
        }

        knockbackVector = Vector3.zero;
        isKnockedback = false;
    }

    public void ApplyMovement()
    {
        Vector3 targetPosition = playerTransform.position - (directionTowardsPlayer * offsetDistanceFromPlayersPosition);
        Vector3 movementDirectionRaw;

        if (isGuided)
        {
            if(canWalkTowardsPlayer)
            {
                movementDirectionRaw = (targetPosition - transform.position).normalized;
            }
            else
            {
                movementDirectionRaw = orderedDirection;
            }
            
            movementDirection = (movementDirectionRaw + enemyAvoidanceOffset).normalized;
            LimitDirections(occupiedSide);
            transform.position += (Time.deltaTime * speed * movementDirection) + knockbackVector;
        }

        else
        {
            movementDirectionRaw = (targetPosition - transform.position).normalized;
            movementDirection = (movementDirectionRaw + enemyAvoidanceOffset).normalized;
            transform.position += (Time.deltaTime * speed * movementDirection) + knockbackVector;
        }
    }

    private LayerMask CombineLayers(LayerMask maskA, LayerMask maskB)
    {
        return maskA | maskB;
    }

    private Vector3 SumDirections(List<Vector3> directionsToAdd)
    {
        Vector3 sumOfDirections = Vector3.zero;

        foreach (Vector3 direction in directionsToAdd)
        {
            sumOfDirections += direction;
        }

        return sumOfDirections;
    }

    private void HandleAvoidance(LayerMask colliderLayer)
    {
        Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, colliderLayer);

        bool obstacleFound = false;

        if (detectedColliders.Length > 0)
        {
            directionsAwayFromOtherEnemies.Clear();

            foreach (Collider2D collider in detectedColliders)
            {
                if ((enemyLayer.value & (1 << collider.gameObject.layer)) != 0 && collider.gameObject != gameObject)
                {
                    Vector3 direction = (transform.position - collider.transform.position).normalized;
                    directionsAwayFromOtherEnemies.Add(direction);
                }

                if ((obstacleLayer.value & (1 << collider.gameObject.layer)) != 0 )
                {
                    obstacleFound = true;
                    HandleAvoidingObject(collider);
                }
            }

            if (!obstacleFound) 
            {
                isGuided = false;
            }

            enemyAvoidanceOffset = SumDirections(directionsAwayFromOtherEnemies).normalized;
        }
    }

    public void LimitDirections(OccupiedSide occupiedSide)
    {
        switch (occupiedSide)
        {
            case OccupiedSide.Left:
                if (movementDirection.x > 0) { movementDirection.x = 0; }
                if (knockbackVector.x > 0) { knockbackVector.x = 0; }
                break;

            case OccupiedSide.Right:
                if (movementDirection.x < 0) { movementDirection.x = 0; }
                if (knockbackVector.x < 0) { knockbackVector.x = 0; }
                break;

            case OccupiedSide.Top:
                if (movementDirection.y < 0) { movementDirection.y = 0; }
                if (knockbackVector.y < 0) { knockbackVector.y = 0; }
                break;

            case OccupiedSide.Bottom:
                if (movementDirection.y > 0) { movementDirection.y = 0; }
                if (knockbackVector.y > 0) { knockbackVector.y = 0; }
                break;

            default:
                break;
        }
    }

    private void HandleAvoidingObject(Collider2D obstacle)
    {
        Vector3 dirToObstacle = obstacle.transform.position - transform.position;
        isGuided = true;
        
        float xDif = Mathf.Abs(dirToObstacle.x) - obstacle.bounds.extents.x;
        float yDif = Mathf.Abs(dirToObstacle.y) - obstacle.bounds.extents.y;
        
        dirToObstacle.Normalize();

        if (xDif > yDif)
        {   
            if (dirToObstacle.x > 0)
            {
                //LEFT SIDE
                occupiedSide = OccupiedSide.Left;

                if (directionTowardsPlayer.x < 0) { canWalkTowardsPlayer = true; return; }
                canWalkTowardsPlayer = false;

                if (directionTowardsPlayer.y > dirToObstacle.y)
                {
                    orderedDirection = Vector3.up;
                }
                else
                {
                    orderedDirection = Vector3.down;
                }
            }
            else
            {
                //RIGHT SIDE
                occupiedSide = OccupiedSide.Right;

                if (directionTowardsPlayer.x > 0) { canWalkTowardsPlayer = true; return; }
                canWalkTowardsPlayer = false;

                if (directionTowardsPlayer.y > dirToObstacle.y)
                {
                    orderedDirection = Vector3.up;
                }
                else
                {
                    orderedDirection = Vector3.down;
                }
            }
        }
        else
        {
            if (dirToObstacle.y > 0)
            {
                //BOTTOM SIDE
                occupiedSide = OccupiedSide.Bottom;
                
                if (directionTowardsPlayer.y < 0) { canWalkTowardsPlayer = true; return; }
                canWalkTowardsPlayer = false;

                if (directionTowardsPlayer.x > dirToObstacle.x)
                {
                    orderedDirection = Vector3.right;
                }
                else
                {
                    orderedDirection = Vector3.left;
                }
            }
            else
            {
                //TOP SIDE
                occupiedSide = OccupiedSide.Top;

                if (directionTowardsPlayer.y > 0) { canWalkTowardsPlayer = true; return; }
                canWalkTowardsPlayer = false;

                if (directionTowardsPlayer.x > dirToObstacle.x)
                {
                    orderedDirection = Vector3.right;
                }
                else
                {
                    orderedDirection = Vector3.left;
                }
            }
        }
    }
}
