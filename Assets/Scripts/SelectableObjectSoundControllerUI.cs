using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableObjectSoundControllerUI : MonoBehaviour, ISubmitHandler, IMoveHandler, IPointerClickHandler
{
    [SerializeField] private AudioClipNameSelector onSelectSound;
    [SerializeField] private AudioClipNameSelector onClickSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(onClickSound.clipName);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        AudioManager.Instance.PlaySound(onClickSound.clipName);
    }

    public void OnMove(AxisEventData eventData)
    {
        AudioManager.Instance.PlaySound(onSelectSound.clipName);
    }

}