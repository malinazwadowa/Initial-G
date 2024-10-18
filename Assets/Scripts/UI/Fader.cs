using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration;

    [SerializeField]
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FadeCoroutine()
    {
        float alpha = canvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(alpha, 0f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
