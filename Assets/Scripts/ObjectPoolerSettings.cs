using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPoolerSettings", menuName = "Pooler/ObjectPoolerSettings")]
public class ObjectPoolerSettings : ScriptableObject
{
    private void OnValidate()
    {
        for (int i = 0; i < pools.Count; ++i)
        {
            pools[i].name = pools[i].objectType.ToString() + " pool";
        }
    }
  
    public List<Pool> pools;
}

[Serializable]
public class Pool
{
    [HideInInspector] public string name;
    public GameObject objectType;
    public int size;
    public int activeObjectsCount;
}
