using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Pool
{
    [HideInInspector] public string name;
    public GameObject objectType;
    public int size;
    public int activeObjectsCount;
    [HideInInspector] public ObjectLists objectLists = new ObjectLists();
}
public class ObjectLists
{
    public List<GameObject> activeObjects = new List<GameObject>();
    public List<GameObject> inactiveObjects = new List<GameObject>();
}