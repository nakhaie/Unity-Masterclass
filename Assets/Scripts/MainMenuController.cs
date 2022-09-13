using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Toggle Male;
    
    private string _username;
    private Singleton _singleton;

    public void Awake()
    {
        _singleton = Singleton.Instance;

        if (_singleton.GetUsername() != String.Empty)
        {
            LoadGame();
        }
    }

    public void Submit()
    {
        _singleton.SetUsername(_username);
        _singleton.SetGender(Male.isOn ? Singleton.EGender.Male : Singleton.EGender.Female);
        
        LoadGame();
    }

    public void SetUsername(string username)
    {
        _username = username;
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
