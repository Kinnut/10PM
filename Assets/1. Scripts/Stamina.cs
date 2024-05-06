using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider staminaSlider;
    public float maxStamina = 100f;
    public float staminaRegenRate = 10f;
    public float sprintStaminaDepletionRate = 20f;
    public float walkStaminaDepletionRate = 5f;

    private float currentStamina;
    private KeyCode sprintKey;

    void Start()
    {
        // ���� �� ���¹̳� �ʱ�ȭ
        currentStamina = maxStamina;

        // �����̴��� �ִ� ���¹̳� ����
        staminaSlider.maxValue = maxStamina;
        UpdateStaminaUI();

        sprintKey = FindObjectOfType<KeyManager>().GetSprintKey();
    }

    void Update()
    {
        // ���¹̳� ���� �� ȸ��
        HandleStamina();
    }

    void HandleStamina()
    {
        if (Input.GetKey(KeySetting.keys[KeyAction.SPRINT]) && currentStamina > 0f) // �޸��� Ű �Է� Ȯ��
        {
            DepleteStamina(sprintStaminaDepletionRate);
        }
        else
        {
            RegenerateStamina(walkStaminaDepletionRate);
        }
    }

    void DepleteStamina(float depletionRate)
    {
        float depletion = depletionRate * Time.deltaTime;

        if (currentStamina > 0f)
        {
            currentStamina = Mathf.Max(0f, currentStamina - depletion);
            UpdateStaminaUI();
        }
    }

    void RegenerateStamina(float regenerationRate)
    {
        float regeneration = regenerationRate * Time.deltaTime;

        if (currentStamina < maxStamina)
        {
            currentStamina = Mathf.Min(maxStamina, currentStamina + regeneration);
            UpdateStaminaUI();
        }
    }

    void UpdateStaminaUI()
    {
        // �����̴� UI ������Ʈ
        staminaSlider.value = currentStamina;
    }
}
