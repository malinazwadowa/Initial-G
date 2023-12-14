using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string dirPath = Path.Combine(Application.dataPath, "Saves");
    public static string filePath = Path.Combine(dirPath, "savefile.json");

    public static void Initialize()
    {
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
    }

    public static void Save()
    {
        Dictionary<string, Dictionary<string, SaveData>> saveData = LoadFile();
        SaveData(saveData);
        SaveFile(saveData);
    }

    public static void Load() 
    {
        Debug.Log("GameLoaded start");
        Dictionary<string, Dictionary<string, SaveData>> saveData = LoadFile();
        LoadData(saveData);
        Debug.Log("GameLoaded end");
    }
    
    private static void SaveData(Dictionary<string, Dictionary<string, SaveData>> saveData) 
    {
        foreach(SaveableEntity entity in UnityEngine.Object.FindObjectsOfType<SaveableEntity>())
        {
            saveData[entity.id] = entity.SaveData();
        }
    }

    private static void LoadData(Dictionary<string, Dictionary<string, SaveData>> saveData) 
    {
        foreach(SaveableEntity entity in UnityEngine.Object.FindObjectsOfType<SaveableEntity>())
        {
            if(saveData.TryGetValue(entity.id, out Dictionary<string, SaveData> value))
            {
                entity.LoadData(value);
            }
        }
    }

    private static Dictionary<string, Dictionary<string, SaveData>> LoadFile()
    {
        if (!File.Exists(filePath))
        {
            return new Dictionary<string, Dictionary<string, SaveData>>();
        }

        try
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string json = File.ReadAllText(filePath);
            
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, SaveData>>>(json, settings);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading data: {e.Message}");
            return null;
        }
    }

    private static void SaveFile(object saveData)
    {
        try
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string saveString = JsonConvert.SerializeObject(saveData, Formatting.Indented, settings);

            File.WriteAllText(filePath, saveString);
            Debug.Log("Save successful");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving data: {e.Message}");
        }
    }
}
