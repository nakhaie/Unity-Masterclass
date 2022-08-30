using System;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text ItemNameField;

    private PlayerController _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        
        _player.PlayerLookAtItem += OnPlayerLookAtItem;
    }

    private void OnPlayerLookAtItem(string itemName)
    {
        ItemNameField.text = itemName;
    }
}
