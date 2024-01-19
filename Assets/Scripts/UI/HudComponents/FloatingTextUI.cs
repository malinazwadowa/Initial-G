using TMPro;
using UnityEngine;

public class FloatingTextUI : MonoBehaviour
{
    [SerializeField] private float fadeOutSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float startFadeOutTimer;

    private TextMeshPro myText;
    private Color baseColor;
    private float baseFontSize;
    private float elapsedTime;

    private void OnEnable()
    {
        myText = GetComponent<TextMeshPro>();
        baseColor = myText.color;
        baseFontSize = myText.fontSize;
        elapsedTime = 0;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        MoveUp();
        FadeOut();
        ZoomIn();

        if(myText.color.a < 0)
        {
            myText.color = baseColor;
            myText.fontSize = baseFontSize;
            ObjectPooler.Instance.DespawnObject(gameObject);
        }
    }

    private void FadeOut()
    {
        if(elapsedTime > startFadeOutTimer)
        {
            Color textColor = myText.color;
            textColor.a -= fadeOutSpeed * Time.deltaTime;
            myText.color = textColor;
        }
    }
    private void ZoomIn()
    {
        if(elapsedTime < startFadeOutTimer)
        {
            myText.fontSize += zoomSpeed * Time.deltaTime;
        }
        else
        {
            myText.fontSize -= zoomSpeed * Time.deltaTime;
        }
    }
    private void MoveUp()
    {
        myText.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
