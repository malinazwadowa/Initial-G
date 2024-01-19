using TMPro;
using UnityEngine;

public class FloatingTextSpawnerUI : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    
    void Start()
    {
        EventManager.OnEnemyDamaged += SpawnText;
        
    }

    private void OnDisable()
    {
        EventManager.OnEnemyDamaged -= SpawnText;
    }

    private void SpawnText(int textvalue, Vector3 position)
    {
        GameObject spawnedText = ObjectPooler.Instance.SpawnObject(floatingTextPrefab, position);
        spawnedText.GetComponent<TextMeshPro>().text = textvalue.ToString();
    }

}
