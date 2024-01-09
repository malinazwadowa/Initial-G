using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SaveableEntity : MonoBehaviour
{
    public string id = string.Empty;

    [ContextMenu("Generate Id")]
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }


    public Dictionary<string, SaveData> SaveData()
    {
        Dictionary<string, SaveData> data = new Dictionary<string, SaveData>();

        foreach(ISaveable saveable in GetComponents<ISaveable>())
        {
            data[saveable.GetType().ToString()] = saveable.SaveMyData();
        }
        
        return data;
    }

    public void LoadData(Dictionary<string, SaveData> dataDictionary)
    {
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();

            if (dataDictionary.TryGetValue(typeName, out SaveData value))
            {
                saveable.LoadMyData(value);
            }
        }
    }
}
