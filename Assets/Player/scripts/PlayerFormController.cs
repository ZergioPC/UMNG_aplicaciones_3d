using System;
using UnityEngine;

/// <summary>
/// Gestiona la alternancia de formas del jugador, conmutando los modelos 3D (visuales)
/// y habilitando/deshabilitando las referencias a las habilidades correspondientes.
/// </summary>
public class PlayerFormController : MonoBehaviour
{
    [Header("Modelos Visuales (Jerarquía)")]
    [Tooltip("Objeto visual para la Forma 1")]
    [SerializeField] private GameObject appearanceNormal;

    [Tooltip("Objeto visual para la Forma 2")]
    [SerializeField] private GameObject appearanceDog;

    [Header("Habilidades Forma 1")]
    [SerializeField] private AbilityDash abilityDash;
    //[SerializeField] private MeleeAttackAbility meleeAttackAbility;

    [Header("Habilidades Forma 2")]
   // [SerializeField] private SniffAbility sniffAbility;

    [Header("Configuración Inicial")]
    [SerializeField] private bool startInFormNormal = true;

    // Estado actual
    private bool isAppearanceNormalActive;

    // Propiedades públicas expuestas para PlayerController
    public bool IsAppearanceNormalActive => isAppearanceNormalActive;
    public AbilityDash AbilityDash => abilityDash;
    //public MeleeAttackAbility MeleeAttackAbility => meleeAttackAbility;
    //public SniffAbility SniffAbility => sniffAbility;

    // Evento opcional para notificar a la UI o VFX/SFX cuando el jugador cambia de forma
    public event Action<bool> OnFormChanged; // true = Forma 1, false = Forma 2

    private void Awake()
    {
        // Autotargeting de componentes si no se asignaron desde el Inspector
        if (abilityDash == null) abilityDash = GetComponent<AbilityDash>();
        //if (meleeAttackAbility == null) meleeAttackAbility = GetComponent<MeleeAttackAbility>();
        //if (sniffAbility == null) sniffAbility = GetComponent<SniffAbility>();
    }

    private void Start()
    {
        // Aplicar el estado inicial al arrancar el juego
        SetForm(startInFormNormal);
    }

    /// <summary>
    /// Alterna la forma activa entre Forma 1 y Forma 2.
    /// Invocado por PlayerController cuando se detecta el input de cambio.
    /// </summary>
    public void ToggleForm()
    {
        SetForm(!isAppearanceNormalActive);
    }

    /// <summary>
    /// Establece explícitamente la forma activa y actualiza modelos y componentes.
    /// </summary>
    /// <param name="activeFormNormal">true para activar la Forma 1; false para la Forma 2.</param>
    public void SetForm(bool activeFormNormal)
    {
        isAppearanceNormalActive = activeFormNormal;

        // 1. Activar / Desactivar modelos 3D
        if (appearanceNormal != null) appearanceNormal.SetActive(isAppearanceNormalActive);
        if (appearanceDog != null) appearanceDog.SetActive(!isAppearanceNormalActive);

        // 2. Habilitar / Deshabilitar componentes de habilidades según la forma activa
        if (abilityDash != null) abilityDash.enabled = isAppearanceNormalActive;
       // if (meleeAttackAbility != null) meleeAttackAbility.enabled = isAppearanceNormalActive;
        //if (sniffAbility != null) sniffAbility.enabled = !isAppearanceNormalActive;

        // 3. Disparar evento de notificación (por ejemplo, para UI o efectos de sonido)
        OnFormChanged?.Invoke(isAppearanceNormalActive);
    }
}