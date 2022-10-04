using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Singleton _singleton;
    private PlayerController _player;
    
    void Awake()
    {
        _singleton = Singleton.Instance;
        _player = FindObjectOfType<PlayerController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Item[] levelItems = GameObject.FindObjectsOfType<Item>();
        
        List<Item> interactedItems = _singleton.LoadInteractData(SceneManager.GetActiveScene().name, levelItems);

        foreach (var interactedItem in interactedItems)
        {
            Debug.Log(interactedItem.ItemName);
            
            switch (interactedItem.ItemInteractType)
            {
                case Item.InteractType.Read:
                    
                    break;
                case Item.InteractType.Take:
                    
                    _player.AddToInventory(interactedItem);
                    interactedItem.gameObject.SetActive(false);
                    
                    break;
                case Item.InteractType.Condition:
                    
                    interactedItem.ConditionDone();
                    
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
