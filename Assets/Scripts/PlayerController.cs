using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public delegate void StringValue(string strValue);
    public delegate void StringWithDataValue(string strValue);
    public event StringValue PlayerLookAtItem;
    public event StringWithDataValue PlayerItemDetail;
    
    public float Speed = 12;
    public float Gravity = -9.81f;

    public Transform GroundCheck;
    public float GroundDistance;
    public LayerMask GroundMask;

    public float JumpHeight = 3f;

    public float InteractionDistance;
    public LayerMask InteractionMask;
    
    private CharacterController _controller;

    private Vector3 _velocity;
    private bool _isGround;
    private bool _lockLocomotion = false;

    private List<Item> _inventory = new List<Item>();
    private Singleton _singleton;

    // Start is called before the first frame update
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _singleton = Singleton.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        Look();
        
        if (!_lockLocomotion)
        {
            Locomotion();
        }
    }

    private void Look()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector2.one / 2.0f);

        string itemName = String.Empty;
        string itemDetail = String.Empty;

        if (Physics.Raycast(ray, out RaycastHit hit, InteractionDistance,InteractionMask))
        {
            Item item = hit.collider.GetComponent<Item>();
            itemName = item.ItemName;

            if (Input.GetMouseButtonDown(0))
            {
                itemDetail = item.GetDetail();
                
                switch (item.ItemInteractType)
                {
                    case Item.InteractType.Read:

                        if (PlayerItemDetail != null)
                        {
                            PlayerItemDetail(itemDetail);
                            _singleton.AddInteractableItem(item);
                        }
                        
                        break;
                    case Item.InteractType.Take:

                        _inventory.Add(item);
                        item.gameObject.SetActive(false);
                        _singleton.AddInteractableItem(item);
                        
                        break;
                    case Item.InteractType.Condition:

                        if (KeyExist(item.ItemDetail))
                        {
                            _singleton.AddInteractableItem(item);
                            item.ConditionDone();
                        }
                        
                        break;
                }

                _singleton.SaveInteractedItem(SceneManager.GetActiveScene().name);
            }
        }
        
        if (PlayerLookAtItem != null)
        {
            PlayerLookAtItem(itemName);
        }

    }

    private bool KeyExist(string itemDetail)
    {
        foreach (var item in _inventory)
        {
            if (item.ItemDetail == itemDetail)
            {
                return true;
            }
        }

        return false;
    }

    public void AddToInventory(Item item)
    {
        _inventory.Add(item);
    }
    
    private void Locomotion()
    {
        _isGround = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (_isGround && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * Speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGround)
        {
            _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }
        
        _velocity.y += Gravity * Time.deltaTime;
        
        _controller.Move(_velocity * Speed * Time.deltaTime);
    }

    public void SetLockLocomotion(bool isLocked)
    {
        _lockLocomotion = isLocked;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * InteractionDistance);
        
        //Gizmos.DrawFrustum(Camera.main.transform.position, 60, 100, 1, 1.7f);
    }
}
