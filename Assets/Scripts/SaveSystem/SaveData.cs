using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public Dictionary<string, Dictionary<string, ObjectData>> coreData;
    public Dictionary<string, Dictionary<string, Dictionary<string, ObjectData>>> profilesData;

    public SaveData()
    {
        this.coreData = new Dictionary<string, Dictionary<string, ObjectData>>();
        this.profilesData = new Dictionary<string, Dictionary<string, Dictionary<string, ObjectData>>>();
    }
}