using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProfileController : MonoBehaviour, ISaveable
{
    private string currentProfileName = null;
    public List<string> profileNames = new List<string>();
    public Dictionary<string, string> profileNameToID = new Dictionary<string, string>();

    public void LoadMyData(ObjectData savedData)
    {
        Debug.Log("profile data loading ! ");
        if (savedData is ProfilesData profilesData)
        {
            currentProfileName = profilesData.currentProfileName;
            profileNames = profilesData.profileNames;
            profileNameToID = profilesData.profileNameToID;
        }
    }

    public ObjectData SaveMyData()
    {
        ProfilesData profilesData = new ProfilesData
        {
            IsCoreData = true,
            currentProfileName = this.currentProfileName,
            profileNames = this.profileNames,
            profileNameToID = this.profileNameToID
    };
        return profilesData;
    }

    [Serializable]
    public class ProfilesData : ObjectData
    {
        public string currentProfileName;
        public List<string> profileNames;
        public Dictionary<string, string> profileNameToID;
    }
    private void Update()
    {
        
    }
    public string GetCurrentProfileName()
    {
        return currentProfileName;
    }
    public string GetCurrentProfileId()
    {
        currentProfileName = currentProfileName.NullIfEmpty();

        if (currentProfileName == null)
        {
            Debug.Log("ustawiam na default");
            string name = new("default");
            currentProfileName = name;
            AddProfile(name);
        }
        Debug.Log($"ZWRACAM CURRENT PROFILE ID {profileNameToID[currentProfileName]}");
        return profileNameToID[currentProfileName];
    }

    public void AddProfile(string profileName)
    {
        string id = Guid.NewGuid().ToString();
        profileNameToID.Add(profileName, id);
    }

    public void DeleteProfile(string profileName)
    {
        string profileId = profileNameToID[profileName];
        profileNameToID.Remove(profileName);
        SaveSystem.DeleteProfileData(profileId);
    }


    public void SwitchProfile(string profileName)
    {
        currentProfileName = profileName;
        SaveSystem.LoadCurrentProfileData();
    }

    public void RenameProfile(string profileName, string newProfileName)
    {
        
        if (profileNameToID.TryGetValue(profileName, out string profileId))
        {
            profileNameToID.Remove(profileName);

            profileNameToID.Add(newProfileName, profileId);

            Debug.Log($"Profile {profileName} renamed to {newProfileName}");

            if (currentProfileName == profileName)
            {
                currentProfileName = newProfileName;
            }
        }
        else
        {
            Debug.Log("Unable to find profile to rename.");
        }

        
    }

    public void WipeMyData()
    {
    }
}
