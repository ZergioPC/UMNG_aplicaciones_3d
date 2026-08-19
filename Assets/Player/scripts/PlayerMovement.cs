using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -19.62f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 verticalVelocity;
    private bool isGrounded;

    public bool IsGrounded => isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Ejecutado directamente desde PlayerController.Update()
    public void ProcessMovement(Vector2 input)
    {
        CheckGroundStatus();
        
        // Movimiento horizontal
        Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Aplicar gravedad
        ApplyGravity();
    }

    public void Jump()
    {
        Debug.Log(isGrounded);
        if (isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void CheckGroundStatus()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        else
        {
            isGrounded = controller.isGrounded;
        }

        if (isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f;
        }
    }

    private void ApplyGravity()
    {
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}