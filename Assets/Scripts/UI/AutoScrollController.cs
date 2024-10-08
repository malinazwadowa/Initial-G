using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AutoScrollController : MonoBehaviour
{
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    [Header("Parent object of the listed objects:")]
    public RectTransform listTransform; // Reference to the RectTransform of the list content

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

            if (selectedObject.transform.IsChildOf(listTransform))
            {
                float normalizedPosition = 1f - (float)selectedObject.transform.parent.GetSiblingIndex() / (float)(listTransform.childCount - 1);

                scrollRect.verticalNormalizedPosition = normalizedPosition;
            }
        }
    }
}
