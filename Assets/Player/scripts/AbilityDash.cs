using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AbilityDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float cooldownTime = 1.5f;

    private CharacterController controller;
    private float nextReadyTime = 0f;
    private bool isDashing = false;

    public bool IsDashing => isDashing;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public bool TryUseAbility()
    {
        if (Time.time < nextReadyTime || isDashing) return false;

        StartCoroutine(PerformDash());
        nextReadyTime = Time.time + cooldownTime;
        return true;
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;

        // Si el personaje se mueve, hace dash hacia la dirección a la que mira
        Vector3 dashDirection = transform.forward;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }
}