using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground_UI : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float speedX, speedY;

    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(speedX, speedY) * Time.deltaTime, image.uvRect.size);
    }
}
