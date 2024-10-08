using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundPanel_UI : MonoBehaviour
{
    public GameObject backgroundImageObject;

    private Vector3 startPosition;
    private int yEndPosition = -280;
    public float speed;

    private void Start()
    {
        startPosition = backgroundImageObject.transform.position;
    }

    void Update()
    {
        Vector3 newPosition = backgroundImageObject.transform.position + Vector3.down * speed * Time.deltaTime;
        newPosition.x +=  - speed/2 * Time.deltaTime;

        backgroundImageObject.transform.position = newPosition;

        if(backgroundImageObject.transform.position.y <= yEndPosition)
        {
            backgroundImageObject.transform.position = startPosition;
        }
    }
}
