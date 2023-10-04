using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PickUpItem pickUpType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        //Debug.Log("picknalem object, teraz efekt oraz pooling go ");
        if(player != null)
        {
            pickUpType.PickUp(collision.gameObject);
            Destroy(gameObject);
        }
        
    }
}
