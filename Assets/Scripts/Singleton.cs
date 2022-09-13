using System;
using UnityEngine;

public class Singleton
{
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
    
    public const string Male = "Mr";
    public const string Female = "Mrs";
    
    private Singleton()
    {
        LoadAll();
        Debug.Log("Singleton Init");
    }

    public void LoadAll()
    {
        
    }

    public void SaveAll()
    {
        PlayerPrefs.Save();
    }

    public void SetUsername(string username)
    {
        PlayerPrefs.SetString(UsernameKey, username);
    }
    
    public void SetGender(EGender gender)
    {
        PlayerPrefs.SetInt(GenderKey, (int)gender);
    }

    public string GetUsername()
    {
        return PlayerPrefs.GetString(UsernameKey,String.Empty);
    }
    
    public EGender GetGender()
    {
        return (EGender)PlayerPrefs.GetInt(GenderKey, 0);
    }

    public string GetFullname()
    {
        return $"{(GetGender() == EGender.Male ? Male : Female)}.{GetUsername()}";
    }
}
