using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPoolerParameters", menuName = "ScriptableObjects/ObjectPooler Parameters")]
public class ObjectPoolerParameters : ScriptableObject
{
    private void OnValidate()
    {
        for (int i = 0; i < pools.Count; ++i)
        {
            pools[i].name = pools[i].objectPrefab.ToString() + " pool";
        }
    }
  
    public List<Pool> pools;
}