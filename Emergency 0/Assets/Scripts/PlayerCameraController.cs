using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraController : MonoBehaviour
{
    public float mouseSensitivityX = 400f;
    public float mouseSensitivityY = 400f;

    public Transform playerOrientation;

    float cameraRotationX;
    float cameraRotationY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //* Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        //* Make cursor invisible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();

        //* Check if the time is frozen in a scene
        if (Time.timeScale == 0)
        {
            //* Lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.None;

            //* Make cursor invisible
            Cursor.visible = true;
        }
    }

    private void RotateCamera()
    {
        //* Check if the time is not frozen in a scene
        if (Time.timeScale != 0)
        {
            //* Get mouse input
            float mouseInputX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivityX;
            float mouseInputY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivityY;

            //* Add X mouse input to Y camera rotation & subtract Y mouse input from X camera rotation
            //* This is the way Unity handles rotations and inputs
            cameraRotationY += mouseInputX;
            cameraRotationX -= mouseInputY;

            //* Do not allow the camera to go up/down more than 90deg
            cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);

            //* Rotate the camera and player orientation
            transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
            playerOrientation.rotation = Quaternion.Euler(0, cameraRotationY, 0);
        }
    }
}
