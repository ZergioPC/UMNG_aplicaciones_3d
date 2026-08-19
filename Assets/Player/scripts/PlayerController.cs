using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerFormController))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference attackAction;

    [Header("Referencias")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerFormController formController;

    private Vector2 moveInput;

    private void Awake()
    {
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        if (formController == null)
            formController = GetComponent<PlayerFormController>();
    }

    private void OnEnable()
    {
        inputActionAsset.Enable();

        moveAction.action.performed += OnMovePerformed;
        moveAction.action.canceled += OnMoveCanceled;

        jumpAction.action.performed += OnJumpPerformed;

        attackAction.action.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= OnMovePerformed;
        moveAction.action.canceled -= OnMoveCanceled;

        jumpAction.action.performed -= OnJumpPerformed;

        attackAction.action.performed -= OnAttackPerformed;

        inputActionAsset.Disable();
    }

    private void Update()
    {
        playerMovement.ProcessMovement(moveInput);
    }

    #region Movimiento

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    #endregion

    #region Salto

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        playerMovement.Jump();
    }

    #endregion

    #region Ataque

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("¡Ataque ejecutado!");
    }

    #endregion
}