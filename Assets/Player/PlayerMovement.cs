using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 5f;
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Awake() => rb = GetComponent<Rigidbody>();

    public void Move(Vector2 input) 
    {
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;
        Vector3 targetVelocity = direction * moveSpeed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    public void Jump() 
    {
        if (isGrounded) 
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate() 
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);
    }
}