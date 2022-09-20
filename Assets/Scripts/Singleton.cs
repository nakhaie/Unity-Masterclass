using System;
using UnityEngine;

public class Singleton
{
    private class PlayerData
    {
        public string Username;
        public EGender Gender;
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

    public string GetUsername()
    {
        return _data.Username;
    }
    
    public EGender GetGender()
    {
        return _data.Gender;
    }

    public string GetFullname()
    {
        return $"{(_data.Gender == EGender.Male ? Male : Female)}.{GetUsername()}";
    }
}
