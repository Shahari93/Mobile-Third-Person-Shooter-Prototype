using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMobileJoystick playerInputs;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float playerRotationSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private void Awake()
    {
        playerInputs = new PlayerMobileJoystick();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    void Update()
    {
        MoveJoystick();
        RotateJoystick();
    }

    private void MoveJoystick()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 playerMovement = playerInputs.Player.Move.ReadValue<Vector2>();

        Vector3 move = new Vector3(playerMovement.x, 0f, playerMovement.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    private void RotateJoystick()
    {
        Vector2 playerRotation = playerInputs.Player.Rotate.ReadValue<Vector2>();

        Vector3 rotate = new Vector3(0, -playerRotation.y * Time.deltaTime * playerRotationSpeed, 0);
        rotate.Normalize();

        if (rotate != Vector3.zero)
        {
            float angle = Mathf.Atan2(playerRotation.x, playerRotation.y) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}