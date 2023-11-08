using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float speedX, speedY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(speedX, speedY) * Time.deltaTime, image.uvRect.size);
    }
}
