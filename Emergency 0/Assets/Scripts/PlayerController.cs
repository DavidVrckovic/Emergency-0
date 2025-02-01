using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 3f;
    public float playerJumpForce = 5f;
    public float raycastDistance = 1.1f;
    private Rigidbody playerRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //* Check if the time is not frozen in a scene
        if (Time.timeScale != 0)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement) * playerSpeed * Time.deltaTime;
        Vector3 newPosition = playerRigidbody.position + playerRigidbody.transform.TransformDirection(movement);

        playerRigidbody.MovePosition(newPosition);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                playerRigidbody.AddForce(0, playerJumpForce, 0, ForceMode.Impulse);
            }

            //* LOG
            Debug.Log("Executed jump function via [Space] key.");
        }
    }

    private bool IsGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, raycastDistance));
    }
}
