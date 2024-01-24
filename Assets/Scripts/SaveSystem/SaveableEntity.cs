using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SaveableEntity : MonoBehaviour
{
    [ReadOnly]
    public string id = string.Empty;

    [ContextMenu("Generate Id")]
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }


    public Dictionary<string, ObjectData> SaveData(bool saveOnlyCore = false)
    {
        Dictionary<string, ObjectData> data = new Dictionary<string, ObjectData>();

        foreach(ISaveable saveable in GetComponents<ISaveable>())
        {
            ObjectData saveData = saveable.SaveMyData();

            if((saveData.IsCoreData && saveOnlyCore) || (!saveData.IsCoreData && !saveOnlyCore))
            {
                data[saveable.GetType().ToString()] = saveData;
            }
        }
        
        return data;
    }

    public void LoadData(Dictionary<string, ObjectData> dataDictionary, bool loadOnlyCore = false)
    {
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();

            if (dataDictionary.TryGetValue(typeName, out ObjectData value))
            {
                if ((!loadOnlyCore && !value.IsCoreData) || (loadOnlyCore && value.IsCoreData))
                {
                    saveable.LoadMyData(value);
                }
            }

        }
    }

    public void WipeProfileData()
    {
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            ObjectData saveData = saveable.SaveMyData();

            bool isCoreData = saveData.IsCoreData;

            if (!isCoreData)
            {
                saveable.WipeMyData();
            }
        }

    }
}
