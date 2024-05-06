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
        // 시작 시 스태미나 초기화
        currentStamina = maxStamina;

        // 슬라이더에 최대 스태미나 설정
        staminaSlider.maxValue = maxStamina;
        UpdateStaminaUI();

        sprintKey = FindObjectOfType<KeyManager>().GetSprintKey();
    }

    void Update()
    {
        // 스태미나 감소 및 회복
        HandleStamina();
    }

    void HandleStamina()
    {
        if (Input.GetKey(KeySetting.keys[KeyAction.SPRINT]) && currentStamina > 0f) // 달리기 키 입력 확인
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
        // 슬라이더 UI 업데이트
        staminaSlider.value = currentStamina;
    }
}
