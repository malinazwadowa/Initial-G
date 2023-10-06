using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playersTransfrom;
    void Start()
    {
        playersTransfrom =  PlayerManager.Instance.GetPlayersFeetTransform();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playersTransfrom.position.x, playersTransfrom.position.y, -10);
    }
}
