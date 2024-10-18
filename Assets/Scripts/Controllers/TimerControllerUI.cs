using TMPro;
using UnityEngine;

public class TimerControllerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public void SetTimerText(float value)
    {
        int minutes = Mathf.FloorToInt(value/60);
        int seconds = Mathf.FloorToInt(value - minutes * 60);

        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
