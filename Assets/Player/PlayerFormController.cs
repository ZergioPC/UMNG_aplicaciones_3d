using UnityEngine;

public enum FormType { Form1, Form2 }

public class PlayerFormController : MonoBehaviour 
{
    public FormType currentForm = FormType.Form1;

    [Header("Visual Models")]
    public GameObject normalForm;
    public GameObject dogForm;

    [Header("Abilities")]
    public MonoBehaviour dashAbility;
    public MonoBehaviour meleeAttackAbility;
    public MonoBehaviour sniffAbility;

    void Start() => UpdateForm();

    public void ToggleForm() 
    {
        currentForm = (currentForm == FormType.Form1) ? FormType.Form2 : FormType.Form1;
        UpdateForm();
    }

    private void UpdateForm() 
    {
        bool isForm1 = currentForm == FormType.Form1;

        // Visuales
        if (normalForm) normalForm.SetActive(isForm1);
        if (dogForm) dogForm.SetActive(!isForm1);

        // Habilidades
        if (dashAbility) dashAbility.enabled = isForm1;
        if (meleeAttackAbility) meleeAttackAbility.enabled = isForm1;
        if (sniffAbility) sniffAbility.enabled = !isForm1;
    }
}