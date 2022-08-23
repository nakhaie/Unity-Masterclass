using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 12;
    public float gravity = -9.81f;

    public Transform GroundCheck;
    public float GroundDistance;
    public LayerMask GroundMask;

    public float JumpHeight = 3f;
    
    private CharacterController _controller;

    private Vector3 velocity;
    private bool isGround;
    
    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        
        _controller.Move(velocity * speed * Time.deltaTime);
    }

}
