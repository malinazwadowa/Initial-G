using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int score = 0;
    //score manager ~

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += UpdateScore;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= UpdateScore;
    }

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Score: {score}";
    }

    private void UpdateScore()
    {
        score++;
        text.text = $"Score: {score}";
    }
}
