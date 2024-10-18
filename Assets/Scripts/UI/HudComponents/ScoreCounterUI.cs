using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        //EventManager.OnEnemyKilled += UpdateScore;
    }

    private void OnDisable()
    {
        //EventManager.OnEnemyKilled -= UpdateScore;
    }

    void Start()
    {
        scoreText.text = $"{LevelManager.Instance.Score}";
    }

    public void UpdateScore()
    {
        scoreText.text = $"Score: {LevelManager.Instance.Score}";
    }

    public void SetScore(int value)
    {
        scoreText.text = $"{value}";
    }
}
