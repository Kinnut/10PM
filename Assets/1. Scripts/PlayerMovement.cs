using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float sprintSpeedMultiplier = 3f; // �޸��� �ӵ� ���� ���
    public float staminaMax = 100f; // �ִ� ���¹̳� ��
    public float staminaRegenRate = 10f;
    public float sprintStaminaDepletionRate = 20f;

    public Slider staminabar;

    float stamina;

    CharacterController cc;

    void Start() // ���� �����Ҷ�
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���

        stamina = staminaMax;

        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    private void Move() // ĳ���� �̵�
    {
        float verticalMovement = 0f;
        float horizontalMovement = 0f;
        float currentSpeed = speed;

        // Ű���� �ʾ�
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.FORWARD]))
            verticalMovement = 0f;
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.BACK]))
            verticalMovement = 0f;
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.LEFT]))
            horizontalMovement = 0f;
        if (Input.GetKeyUp(KeySetting.keys[KeyAction.RIGHT]))
            horizontalMovement = 0f;

        // ���ο� Ű �Է�
        if (Input.GetKey(KeySetting.keys[KeyAction.FORWARD]))
            verticalMovement = 1f;
        if (Input.GetKey(KeySetting.keys[KeyAction.BACK]))
            verticalMovement = -1f;
        if (Input.GetKey(KeySetting.keys[KeyAction.LEFT]))
            horizontalMovement = -1f;
        if (Input.GetKey(KeySetting.keys[KeyAction.RIGHT]))
            horizontalMovement = 1f;

        if (Input.GetKey(KeySetting.keys[KeyAction.SPRINT]) && stamina > 0)
        {
            currentSpeed *= sprintSpeedMultiplier;
            DepleteStamina(sprintStaminaDepletionRate);
        }
        else
        {
            RegenerateStamina();
        }

        Vector3 move = transform.forward * verticalMovement + transform.right * horizontalMovement;
        cc.Move(move * currentSpeed * Time.deltaTime);
    }

    void UpdateStamina()
    {
        if (stamina < staminaMax)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, staminaMax);
            staminabar.value = stamina;
        }
    }

    void DepleteStamina(float depletionRate)
    {
        stamina -= depletionRate * Time.deltaTime;
        staminabar.value = stamina;
    }

    void RegenerateStamina() 
    {
        if (stamina < staminaMax)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, staminaMax);
            staminabar.value = stamina;
        }
    }
}
