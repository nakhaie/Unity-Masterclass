using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public enum Panel
    {
        Normal = 0,
        Detail = 1,
        Pause = 2
    }

    [Header("Panels")]
    public GameObject InteractPanel;
    public GameObject DetailPanel;
    public GameObject PausePanel;
    
    [Header("Fields")]
    public Text ItemNameField;
    public Text ItemDetailField;

    private PlayerController _player;
    private CameraController _camera;
    private string _username;

    private Panel _curPanel;
    private Singleton _singleton;
    
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _camera = Camera.main.GetComponent<CameraController>();
        _singleton = Singleton.Instance;
        
        _player.PlayerLookAtItem += OnPlayerLookAtItem;
        _player.PlayerItemDetail += OnPlayerItemDetail;

        _username = _singleton.GetFullname();

        SetPanel(Panel.Normal);
        _player.SetLockLocomotion(false);
        _camera.SetLockMotion(false);
    }

    private void Update()
    {
        switch (_curPanel)
        {
            case Panel.Normal:
                if (Input.GetButtonDown("Cancel"))
                {
                    Pause();
                }
                break;
            case Panel.Detail:
                if (Input.GetButtonDown("Cancel"))
                {
                    Detail();
                }
                break;
            case Panel.Pause:
                if (Input.GetButtonDown("Cancel"))
                {
                    Resume();
                }
                break;
            
        }
    }

    public void Resume()
    {
        SetPanel(Panel.Normal);
        Time.timeScale = 1;
                    
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        SetPanel(Panel.Pause);
                    
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
                    
        Time.timeScale = 0;
    }

    public void Detail()
    {
        SetPanel(Panel.Normal);
        _player.SetLockLocomotion(false);
        _camera.SetLockMotion(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    private void OnPlayerItemDetail(string itemDetail)
    {
        SetPanel(Panel.Detail);
        _player.SetLockLocomotion(true);
        _camera.SetLockMotion(true);

        ItemDetailField.text = itemDetail;
    }

    private void OnPlayerLookAtItem(string itemName)
    {
        ItemNameField.text = itemName;
    }

    private void SetPanel(Panel panel)
    {
        _curPanel = panel;

        InteractPanel.SetActive(_curPanel == Panel.Normal);
        DetailPanel.SetActive(_curPanel == Panel.Detail);
        PausePanel.SetActive(_curPanel == Panel.Pause);
    }
}
