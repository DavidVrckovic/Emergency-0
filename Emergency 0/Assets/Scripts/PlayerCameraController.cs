using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraController : MonoBehaviour
{
    public float mouseSensitivity = 2;
    public float smoothing = 2;
    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //* Check if the time is not frozen in a scene
        if (Time.timeScale != 0)
        {
            RotateCamera();
        }
    }

    private void RotateCamera()
    {
        Vector2 inputValue = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputValue = Vector2.Scale(inputValue, new Vector2(mouseSensitivity * smoothing, mouseSensitivity * smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValue.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValue.y, 1f / smoothing);

        currentLookingPosition += smoothedVelocity;

        transform.localRotation = Quaternion.AngleAxis(-currentLookingPosition.y, Vector3.right);

        player.transform.localRotation = Quaternion.AngleAxis(currentLookingPosition.x, player.transform.up);
    }
}
