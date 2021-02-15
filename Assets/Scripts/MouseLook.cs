using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public Transform playerBody;
	public float mouseSensitivityX;
	public float mouseSensitivityY;
	public float cameraDamping;

	private float xRotation;
	private float yRotation;


	void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime * 100;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime * 100;

        // left-right look
        yRotation += mouseX;

        // up-down look
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Lerp(
	        transform.localRotation, 
	        Quaternion.Euler(xRotation, 0, 0), 
	        Time.deltaTime * cameraDamping);
        
        playerBody.localRotation = Quaternion.Lerp(
	        playerBody.localRotation,
	        Quaternion.Euler(0, yRotation, 0),
	        Time.deltaTime * cameraDamping);
        
    }
}
