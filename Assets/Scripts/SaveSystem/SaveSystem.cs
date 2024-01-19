using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string dirPath = Path.Combine(Application.dataPath, "Saves");
    public static string filePath = Path.Combine(dirPath, "savefile.json");
    
    private static SaveData lastLoadedData;
    private static string selectedProfileID;


    public static void Initialize()
    {
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
    }
    

    public static void Save()
    {
        SaveData savedData = LoadFile();

        SaveData(savedData);

        SaveFile(savedData);
    }

    public static void Load()
    {
        Debug.Log("GameLoaded start");

        SaveData savedData = LoadFile();

        LoadCoreData(savedData);
        selectedProfileID = GameManager.Instance.profileController.GetCurrentProfileId();

        LoadProfileData(savedData);
        lastLoadedData = savedData;
      
        Debug.Log($"GameLoaded end selected profiles ID is: {selectedProfileID}");
    }

    public static void LoadCurrentProfileData()
    {
        Save();

        selectedProfileID = GameManager.Instance.profileController.GetCurrentProfileId();
        Debug.Log("loading profile wiping data next");
        WipeProfileData();
        
        Load();
    }

    public static void DeleteProfileData(string profileId)
    {
        SaveData savedData = LoadFile();

        SaveData(savedData);

        savedData.profilesData.Remove(profileId);

        SaveFile(savedData);

        Load();
    }

    private static void SaveData(SaveData saveData) 
    {
        foreach(SaveableEntity entity in UnityEngine.Object.FindObjectsOfType<SaveableEntity>())
        {
            if(entity.SaveData(true).Count > 0)
            {
                saveData.coreData[entity.id] = entity.SaveData(true);
            }
            
            Dictionary<string, Dictionary<string, ObjectData>> entityData;
            Debug.Log($"ZAPISUJE DO PROFILE ID > {selectedProfileID} ");
            if (saveData.profilesData.ContainsKey(selectedProfileID))
            {
                entityData = saveData.profilesData[selectedProfileID];
            }
            else
            {
                entityData = new Dictionary<string, Dictionary<string, ObjectData>>();
                saveData.profilesData[selectedProfileID] = entityData;
            }

            entityData[entity.id] = entity.SaveData(false);
        }
    }

    private static void LoadCoreData(SaveData saveData) 
    {
        foreach(SaveableEntity entity in UnityEngine.Object.FindObjectsOfType<SaveableEntity>())
        {
            if(saveData.coreData.TryGetValue(entity.id, out Dictionary<string, ObjectData> value))
            {
                entity.LoadData(value, true);
            }
        }
    }

    private static void LoadProfileData(SaveData savedData) 
    {
        Debug.Log($"laduje data profile");
        Dictionary<string, Dictionary<string, ObjectData>> profileData;

        Debug.Log($"zaladwoane profile i sprawdzilem czy jest profil dla {selectedProfileID} no i jesli jest to to powinno byc : bnnuuumerki ile profili {savedData.profilesData.Count} ");

        if (savedData.profilesData.ContainsKey(selectedProfileID))
        {
            Debug.Log($"profiles data ma wpis dla {selectedProfileID}");

           profileData = savedData.profilesData[selectedProfileID];
        }
        else
        {
            profileData = new Dictionary<string, Dictionary<string, ObjectData>>();
        }

        foreach (SaveableEntity entity in UnityEngine.Object.FindObjectsOfType<SaveableEntity>())
        {
            if (profileData.TryGetValue(entity.id, out Dictionary<string, ObjectData> entityData))
            {
                entity.LoadData(entityData);
            }
        }
    }
    
    private static void WipeProfileData()
    {
        foreach (SaveableEntity entity in UnityEngine.Object.FindObjectsOfType<SaveableEntity>())
        {
            entity.WipeProfileData();
        }
    }

    private static SaveData LoadFile()
    {
        if (!File.Exists(filePath))
        {
            return new SaveData();
        }

        try
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string json = File.ReadAllText(filePath);
            
            return JsonConvert.DeserializeObject<SaveData>(json, settings);
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
