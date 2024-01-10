using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    [SerializeField] private float arcingDuration;
    [SerializeField] public CollectibleType type;

    protected Player player;
    private Transform playerTransform;
    
    private bool hasCollided = false;

    private float elapsedTime;
    private float lerpFactor;

    private void OnEnable()
    {
        hasCollided = false;
        elapsedTime = 0;
        player = null;
    }

    private void Update()
    {
        if(hasCollided)
        {
            MoveObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.player = collision.gameObject.GetComponentInParent<Player>();

        if (player == null)
        {
            return;
        }
        else
        {
            playerTransform = PlayerManager.Instance.GetPlayersCenterTransform();

            hasCollided = true;
        }
    }

    protected virtual void Collect()
    {
        GameManager.Instance.gameStatsController.RegisterCollectiblePickUp(type);
    }

    private void MoveObject()
    {
        elapsedTime += Time.deltaTime;
        
        lerpFactor = Mathf.Clamp01(elapsedTime / arcingDuration);

        Vector3 direction = Vector3.Lerp( - (playerTransform.position - transform.position).normalized * 1.1f, (playerTransform.position - transform.position).normalized * 2.5f, lerpFactor);

        transform.position += Time.deltaTime * travelSpeed * direction;

        if (Vector3.Distance(transform.position, playerTransform.position) < 0.2f)
        {
            Collect();
        }
    }
}
