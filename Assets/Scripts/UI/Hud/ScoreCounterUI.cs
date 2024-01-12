using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        EventManager.OnEnemyKilled += UpdateScore;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyKilled -= UpdateScore;
    }

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Score: {LevelManager.Instance.Score}";
    }

    private void UpdateScore()
    {
        text.text = $"Score: {LevelManager.Instance.Score}";
    }
}
