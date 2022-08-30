using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    
    public float CameraAngleLimitMin;
    public float CameraAngleLimitMax;
    
    public Transform playerBody;
    private float xRotation = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, CameraAngleLimitMin, CameraAngleLimitMax);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f,0.0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
