using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private string _username;
    
    public void Submit()
    {
        PlayerPrefs.SetString("username", _username);
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
