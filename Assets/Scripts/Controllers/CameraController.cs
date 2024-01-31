using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playersTransform;

    void Start()
    {
        playersTransform =  PlayerManager.Instance.GetPlayersCenterTransform();
    }

    void Update()
    {
        transform.position = new Vector3(playersTransform.position.x, playersTransform.position.y, -10);
    }
}
