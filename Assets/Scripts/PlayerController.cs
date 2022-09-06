using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public delegate void StringValue(string strValue);
    public delegate void StringWithDataValue(string strValue, UiController.GameData[] data);
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
    
    // Start is called before the first frame update
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
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
        UiController.GameData[] detailData = new UiController.GameData[0];
        
        if (Physics.Raycast(ray, out RaycastHit hit, InteractionDistance,InteractionMask))
        {
            switch (hit.collider.tag)
            {
                case "Note":

                    NoteController note = hit.collider.GetComponent<NoteController>();
                    itemName = note.itemName;
                    itemDetail = note.itemDetail;
                    detailData = note.GameDataInDetail;
                    break;
            }

            if (itemDetail != string.Empty && Input.GetMouseButtonDown(0))
            {
                if (PlayerItemDetail != null)
                {
                    PlayerItemDetail(itemDetail, detailData);
                }
            }
        }
        
        if (PlayerLookAtItem != null)
        {
            PlayerLookAtItem(itemName);
        }

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
