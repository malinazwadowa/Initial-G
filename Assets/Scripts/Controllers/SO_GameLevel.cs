using NaughtyAttributes;
using UnityEngine;


[CreateAssetMenu(fileName = "newGameLevel", menuName = "ScriptableObjects/GameLevel")]
public class SO_GameLevel : ScriptableObject
{
    public GameLevel type;
    public SceneName myScene;

    [Header("Duration in minutes:")]
    public float duration;

    [Header("Enemy wave manager settings:")]
    [Expandable] public SO_EnemyWaveManagerParameters myEnemyWaveManagerParameters;

    [Header("Audio clips for the scene:")]
    public AudioClipNameSelector levelMusic;

    [Header("Unlocked by completing:")]
    public GameLevel unlockedBy;
}
 