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
                
                break;
            case Panel.Detail:
                if (Input.GetButtonDown("Cancel"))
                {
                    SetPanel(Panel.Normal);
                    _player.SetLockLocomotion(false);
                    _camera.SetLockMotion(false);
                }
                break;
        }
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
    }
}
