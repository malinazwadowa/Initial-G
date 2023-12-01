using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string dirPath = Path.Combine(Application.dataPath, "Saves");
    public static string path = Path.Combine(dirPath, "savefile.json");

    public static void Initialize()
    {
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
    }

    public static void Save(SaveData saveData)
    {
        try
        {
            string saveString = JsonConvert.SerializeObject(saveData);
            File.WriteAllText(path, saveString);
            Debug.Log("Save successful");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving data: {e.Message}");
        }
    }

    public static SaveData Load()
    {
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);

                SaveData loadedData = JsonConvert.DeserializeObject<SaveData>(json);

                return loadedData;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading data: {e.Message}");
                return null;
            }
        }

        Debug.LogWarning("No save file found.");
        return null;
    }
}
