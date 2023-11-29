using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public PickUpableItem pickUpType;

    public float travelSpeed;
    public float arcingDuration;

    private Transform playerTransform;
    private Player player;
    
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

    private void Collect()
    {
        pickUpType.OnPickUp(player.gameObject);
        ObjectPooler.Instance.DespawnObject(gameObject);
    }

    private void MoveObject()
    {
        elapsedTime += Time.deltaTime;
        
        lerpFactor = Mathf.Clamp01(elapsedTime / arcingDuration);

        Vector3 direction = Vector3.Lerp( - (playerTransform.position - transform.position).normalized * 1.1f, (playerTransform.position - transform.position).normalized * 2.5f, lerpFactor);

        transform.position += direction * Time.deltaTime * travelSpeed;

        if (Vector3.Distance(transform.position, playerTransform.position) < 0.2f)
        {
            Collect();
        }
    }
}
