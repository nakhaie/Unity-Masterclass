using System;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private class PlayerData
    {
        public string Username;
        public EGender Gender;
        public List<Item> Interacted = new List<Item>();
    }

    private static Singleton _instance;

    public static Singleton Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            
            _instance = new Singleton();
            return _instance;
        }
    }
    
    public enum EGender
    {
        Male = 0,
        Female = 1
    }

    private const string UsernameKey = "username";
    private const string GenderKey = "gender";
    private const string InteractedKey = "inventory";
    private char SeparatorItem = '#';

    private PlayerData _data = new PlayerData();
    
    public const string Male = "Mr";
    public const string Female = "Mrs";
    
    private Singleton()
    {
        LoadAll();
        Debug.Log("Singleton Init");
    }

    public void LoadAll()
    {
        _data.Username = PlayerPrefs.GetString(UsernameKey, String.Empty);
        _data.Gender = (EGender)PlayerPrefs.GetInt(GenderKey, 0);
    }

    public void SaveAll()
    {
        PlayerPrefs.Save();
    }

    public List<Item> LoadInteractData(string levelName, Item[] levelItems)
    {
        string InteractedDataStr = PlayerPrefs.GetString($"{InteractedKey}_{levelName}", String.Empty);
        
        
        if (!string.IsNullOrEmpty(InteractedDataStr))
        {
            string[] interactedData = InteractedDataStr.Split(SeparatorItem);

            foreach (var item in levelItems)
            {
                foreach (var data in interactedData)
                {
                    if (item.ItemName == data)
                    {
                        AddInteractableItem(item);
                        break;
                    }
                }
            }
        }
        
        Debug.Log($"_data.Interacted: {_data.Interacted.Count}");
        
        return _data.Interacted;
    }
    
    public void RemoveAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SetUsername(string username)
    {
        _data.Username = username;
        PlayerPrefs.SetString(UsernameKey, username);
    }
    
    public void SetGender(EGender gender)
    {
        _data.Gender = gender;
        PlayerPrefs.SetInt(GenderKey, (int)gender);
    }

    public void AddInteractableItem(Item item)
    {
        _data.Interacted.Add(item);
    }

    public void SaveInteractedItem(string levelName)
    {
        string result = String.Empty;

        foreach (var item in _data.Interacted)
        {
            result += $"{item.ItemName}{SeparatorItem}";
        }
        
        PlayerPrefs.SetString($"{InteractedKey}_{levelName}" , result);
        PlayerPrefs.Save();
        Debug.Log(result);
    }

    public string GetUsername()
    {
        return _data.Username;
    }
    
    public EGender GetGender()
    {
        return _data.Gender;
    }

    public List<Item> GetCurInteracted()
    {
        return _data.Interacted;
    }
    
    public string GetFullname()
    {
        return $"{(_data.Gender == EGender.Male ? Male : Female)}.{GetUsername()}";
    }
}
