using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[RequireComponent(typeof(Button))]
public class ButtonSoundControllerUI : MonoBehaviour, IPointerClickHandler, ISelectHandler
{
    [SerializeField] private AudioClipNameSelector onSelectSound;
    [SerializeField] private AudioClipNameSelector onClickSound;
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(onClickSound.clipName);
        Debug.Log("klikam");
    }

    public void OnSubmit(BaseEventData eventData)
    {
        AudioManager.Instance.PlaySound(onClickSound.clipName);
    }

    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.Instance.PlaySound(onSelectSound.clipName);
    }
}