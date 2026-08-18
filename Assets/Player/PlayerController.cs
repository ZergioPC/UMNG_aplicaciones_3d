using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Action Maps")]
    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _atackAction;

    [Header("Referencias de Componentes")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerFormController formController;

    private void OnEnable()
    {
        _inputActionAsset.Enable();    
    }

    private void OnDisable() 
    {
        _inputActionAsset.Disable();
    }

    private void Awake()
    {
        // Auto-asignación de componentes si no están vinculados en el Inspector
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        if (formController == null) formController = GetComponent<PlayerFormController>();
        //if (dashAbility == null) dashAbility = GetComponent<DashAbility>();
        //if (meleeAbility == null) meleeAbility = GetComponent<MeleeAttackAbility>();
        //if (sniffAbility == null) sniffAbility = GetComponent<SniffAbility>();

        _moveAction.action.performed += OnMovePerformed;
        _moveAction.action.canceled += OnMoveCanceled;

        _jumpAction.action.performed += OnJumpPerformed;
        _jumpAction.action.canceled += OnJumpCanceled;

        _atackAction.action.performed += OnAtackPerformed;
        _atackAction.action.canceled += OnAtackCanceled;
    }

    private void OnDestroy()
    {
        _moveAction.action.performed -= OnMovePerformed;
        _moveAction.action.canceled -= OnMoveCanceled;

        _jumpAction.action.performed -= OnJumpPerformed;
        _jumpAction.action.canceled -= OnJumpCanceled;

        _atackAction.action.performed -= OnAtackPerformed;
        _atackAction.action.canceled -= OnAtackCanceled;
    }

    #region Movimiento
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Para el movimiento normalmente leemos un Vector2 (WASD o Stick)
        var _moveInput = context.ReadValue<Vector2>();

        // 3. (Opcional) Disparamos la animación de caminar
        if (_animator != null)
        {
            _animator.SetBool("isMoving", true);
            _animator.SetFloat("moveX", _moveInput.x);
            _animator.SetFloat("moveY", _moveInput.y);
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Al soltar las teclas/stick, reiniciamos el movimiento a cero
        var _moveInput = Vector2.zero;
    }
    #endregion

    #region Salto
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        // Aquí ejecutas la lógica para saltar
        Debug.Log("¡Salto presionado!");
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        // Útil para saltos de altura variable (al soltar el botón antes)
        Debug.Log("Salto soltado");
    }
    #endregion

    #region Ataque
    private void OnAtackPerformed(InputAction.CallbackContext context)
    {
        // Lógica de ataque
        Debug.Log("¡Ataque ejecutado!");
    }

    private void OnAtackCanceled(InputAction.CallbackContext context)
    {
        // Lógica si necesitas detectar cuando se suelta el botón de ataque
        Debug.Log("Ataque detenido/soltado");
    }
    #endregion

    private void Update()
    {
        //ProcessMovement();
        //ProcessFormSwitch();
        //ProcessAbilities();
    }

    private void ProcessMovement()
    {
        //Vector2 inputDirection = playerInput.GetMoveInput();
        //playerMovement.SetMoveInput(inputDirection);

        //if (playerInput.GetJumpPressed())
        //{
            //playerMovement.Jump();
        //}
    }

}