using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 70f;
    public float groundDrag = 5f;

    public Transform playerOrientation;

    float horizontalInput;
    float verticalInput;
    Vector3 movementDirection;
    Rigidbody playerRigidbody;
    
    [SerializeField] private AudioSource stepAudioSource;



    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    bool grounded;



    [Header("Jumping")]
    public float jumpForce = 8f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.05f;
    bool readyToJump = true;



    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //* Get the rigidbody and freeze its rotation
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //* Check if the time is not frozen in a scene
        if (Time.timeScale != 0)
        {
            //* Ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            GetInput();
            PlayerSpeedControl();

            //* Handle the drag
            if (grounded)
                playerRigidbody.linearDamping = groundDrag;
            else
                playerRigidbody.linearDamping = 0;
        }
    }

    void FixedUpdate()
    {
        //* Check if the time is not frozen in a scene
        if (Time.timeScale != 0)
        {
            MovePlayer();
        }
    }

    private void GetInput()
    {
        //* Get horizontal and vertical keyboard inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //* Check if the jump key is pressed
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            PlayerJump();

            //* Ability to continuously hold the jump key to perform a jump
            Invoke(nameof(ResetPlayerJump), jumpCooldown);

            //* LOG
            Debug.Log("Player jump executed.");
        }

        //* Check if the WASD keys are pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            stepAudioSource.enabled = true;
            if (Input.GetKey(jumpKey) && readyToJump && grounded)
            {
                stepAudioSource.enabled = false;
            }
        }
        else
        {
            stepAudioSource.enabled = false;
        }
    }

    private void MovePlayer()
    {
        //* Calculate movement direction
        movementDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        //* Add force to the player (on ground / in the air)
        if (grounded)
            playerRigidbody.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Force);
        else if (!grounded)
            playerRigidbody.AddForce(movementDirection.normalized * movementSpeed * airMultiplier, ForceMode.Force);
    }

    private void PlayerSpeedControl()
    {
        //* Get the flat velocity of a Rigidbody
        Vector3 flatVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);

        //* If the flat velocity is greater than the movement speed
        if (flatVelocity.magnitude > movementSpeed)
        {
            //* Calculate a maximum (limit) velocity and apply it to the Rigidbody
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, playerRigidbody.linearVelocity.y, limitedVelocity.x);
        }
    }

    private void PlayerJump()
    {
        //* Set Rigidbody Y velocity to 0 to make sure a jump is always the same height
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);

        //* Add upward force to the player
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetPlayerJump()
    {
        readyToJump = true;
    }
}
